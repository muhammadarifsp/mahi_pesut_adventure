using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwipeMenu : MonoBehaviour
{
    public GameObject scrollbar;
    public static float scroll_pos = 0;
    float[] pos;
    float distance,
        time;
    int active_menu = 0,
        active_menu_scroll = 0;
    bool run_time = false;

    private void Start()
    {
        scroll_pos = 0;
        // ngecek jumlah item scroll
        pos = new float[transform.childCount];
        // memberi jarak tiap item
        distance = 1f / (pos.Length - 1f);
        // memasukan nilai jarak ke dalam tiap array dari tiap item dalam index
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

    void Update()
    {
        scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(
            scrollbar.GetComponent<Scrollbar>().value,
            pos[active_menu],
            0.1f
        );

        // buat mengecil dan membesarkan skala dari item, sehingga kelihatan di highlight
        for (int i = 0; i < pos.Length; i++)
        {
            if (
                (i == 0 && pos[active_menu] < 0)
                || (i == pos.Length - 1 && pos[active_menu] > pos[i])
            ) // jika posisi scroll melewati posisi konten menu awal atau akhir dianggap menu tersebut yang dipilih
            {
                transform.GetChild(i).localScale = Vector2.Lerp(
                    transform.GetChild(i).localScale,
                    new Vector2(1f, 1f),
                    0.1f
                );
            }

            if (
                pos[active_menu] < pos[i] + (distance / 2)
                && pos[active_menu] > pos[i] - (distance / 2)
            )
            {
                transform.GetChild(i).localScale = Vector2.Lerp(
                    transform.GetChild(i).localScale,
                    new Vector2(1f, 1f),
                    0.1f
                );

                MenuManager.active_menu = i; // set tulisan menu di button sesuai dengan konten menu yang di highlight
                for (int j = 0; j < pos.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(
                            transform.GetChild(j).localScale,
                            new Vector2(.8f, .8f),
                            0.1f
                        );
                    }
                }
            }
        }
    }

    public void Next() // untuk tombol next menu
    {
        // Debug.Log(active_menu_scroll);
        // active_menu = active_menu_scroll; // set menu aktif dengan menu aktif yang sudah di skroll, sebagai acuan
        if (active_menu < pos.Length - 1) // cek apakah menu aktifnya tidak melebihi index terakhir pada konten menu
        {
            active_menu += 1;
            // scroll_pos = pos[active_menu]; // ubah posisi skroll dengan konten menu yang lagi aktif
            // time = 0; // atur waktu run time dari 0
            // run_time = true; // atur run time jadi true, sehingga kondisi sebelumnya jadi aktif
        }
    }

    public void Prev() // untuk tombol prev menu
    {
        // Debug.Log(active_menu_scroll);
        // active_menu = active_menu_scroll;
        if (active_menu > 0)
        {
            active_menu -= 1;
            // scroll_pos = pos[active_menu];
            // time = 0;
            // run_time = true;
        }
    }

    private void scrollCode()
    {
        // aktif kalo navigasi menu diklik
        if (run_time)
        {
            for (int i = 0; i < pos.Length; i++)
            {
                // buat check konten menu apa yang dominan di tengah
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    // membuat gerakan perpindahan konten menu lebih smooth
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(
                        scrollbar.GetComponent<Scrollbar>().value,
                        pos[i],
                        0.1f
                    );
                }
            }
            time += Time.deltaTime; // buat pengecekan waktu
            if (time > 1f) // waktu dicek setelah 1f atau 1 detik baru bisa di scroll lagi
            {
                run_time = false; // matikan kondisi
                time = 0; // set ulang
                active_menu_scroll = active_menu; // set menu yang aktif buat skroll sesuai dengan menu yang aktif ketika habis klik
            }
        }
        // aktif jika klik dan tahan
        if (Input.GetMouseButton(0))
        {
            // ngambil nilai posisi skroll
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
            // Debug.Log("Scroll pos = " + scroll_pos);
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(
                        scrollbar.GetComponent<Scrollbar>().value,
                        pos[i],
                        0.1f
                    );
                    active_menu_scroll = i; // set aktif skroll sesuai dengan kondisi di atas
                }
            }
        }
    }
}
