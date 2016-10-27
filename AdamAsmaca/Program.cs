using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdamAsmaca
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Kelime havuzundan rastgele kelime seçilecek ve harf sayısı x 100 şeklinde puanı olacak
            /// Secilen kelime uzunluğunda _____ şeklinde ekran çıkacak
            /// kullanıcı 1 harf veya 1 kelime girecek
            /// giriş yanlış ise puan %10 oranında azalacak
            /// 6 yanlış denemede bilemezse kaybedecek.

            string[] kelimeler = { "karahindiba", "çanakkale", "kapıağası", "yeniçeri", "arnavutluk", "kardiyoloji" };
            do // 1 oyunluk 
            {
                string kelime = RastgeleKelimeVer(kelimeler);
                int puan = kelime.Length * 100, hak = 6;
                bool bildiMi = false;
                string[] bilinmeyen = new string[kelime.Length];
                for (int i = 0; i < kelime.Length; i++)
                {
                    bilinmeyen[i] = " _ ";
                }
                do
                {
                    EkranaBas(bilinmeyen, puan, hak);
                    TahminEt(kelime, ref puan, ref hak, ref bilinmeyen, out bildiMi);
                } while (hak > 0 && !bildiMi);

                if (bildiMi)
                    Console.WriteLine($"Tebrikler bildiniz :)\nPuanınız: {puan}");
                else
                    Console.WriteLine("Bilemedin bari tekrar oyna!");

                Console.WriteLine("Çıkmak için \"h\" tuşuna basınız");
                ConsoleKeyInfo secim = Console.ReadKey();
                if (secim.Key == ConsoleKey.H)
                    break;
                else
                    Console.WriteLine("Yeni Oyun Başlıyor...");
            } while (true);
        }

        static void TahminEt(string kelime, ref int puan, ref int hak, ref string[] bilinmeyen, out bool bildiMi)
        {
            Console.Write("Tahmininizi Giriniz: ");
            string tahmin = Console.ReadLine();
            if (tahmin.Length == 0)
            {
                hak--;
                puan = Convert.ToInt32(Math.Round(puan - (puan * 0.1)));
                bildiMi = false;
            }
            else if (tahmin.Length > 1)
            {
                if (tahmin.ToLower() == kelime.ToLower())
                    bildiMi = true;
                else
                {
                    hak--;
                    puan = Convert.ToInt32(Math.Round(puan - (puan * 0.1)));
                    bildiMi = false;
                }
            }
            else
            {
                bool harfVarMi = false;
                for (int i = 0; i < kelime.Length; i++)
                {
                    if (kelime.ToLower()[i].ToString() == tahmin.ToLower())
                    {
                        bilinmeyen[i] = $" {tahmin.ToUpper()} ";
                        harfVarMi = true;
                    }
                }
                if (!harfVarMi)
                {
                    hak--;
                    puan = Convert.ToInt32(Math.Round(puan - (puan * 0.1)));
                    bildiMi = false;
                }
                int sonuc = Array.IndexOf(bilinmeyen, " _ ");
                if (sonuc == -1)
                    bildiMi = true;
                else
                    bildiMi = false;
            }
        }
        static string RastgeleKelimeVer(string[] kelimeler) => kelimeler[new Random().Next(0, kelimeler.Length)];
        static void EkranaBas(string[] bilinmeyen, int puan, int hak)
        {
            Console.WriteLine();
            for (int i = 0; i < bilinmeyen.Length; i++)
            {
                Console.Write(bilinmeyen[i]);
            }
            Console.WriteLine($"Toplam Puan: {puan} Kalan Hak: {hak}");
        }
    }
}
