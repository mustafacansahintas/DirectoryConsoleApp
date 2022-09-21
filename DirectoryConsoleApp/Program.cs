using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Info info = new Info();

                Console.WriteLine("Ana Menü");
                info.Space();
                Console.WriteLine("1 - Telefon Rehberi \n2 - Yeni Ekle \n3 - Bul \n4 - Sil \n0 - Çıkış ");
                info.Space();
                Console.WriteLine("Lütfen Tuşlama yapınız");
                info.Space();
                int key = Convert.ToInt32(Console.ReadLine());
                if (key == 1)
                {

                    info.Listcontrol(info.Printlist());

                    info.Backmenu();

                }
                else if (key == 2)
                {
                    while (true)
                    {
                        int a = info.Add();

                        if (a > 0)
                        {
                            info.Listcontrol(info.Printlist());
                            info.Space();
                            Console.WriteLine("Yeni Bir Kayıt için 1 \nAnamenü İçin 0");
                            int key2 = Convert.ToInt32(Console.ReadLine());

                            if (key2 == 1)
                            {
                                continue;
                            }
                            else if (key2 == 0)
                            {
                                info.clear();
                                break;
                            }
                            else
                            {
                                info.clear();
                                info.Fault();
                                break;
                            }

                        }
                        else
                        {
                            info.clear();
                            break;
                        }
                    }

                }
                else if (key == 3)
                {
                    info.clear();
                    Console.WriteLine("Aramak İstediğiniz Kişinin Bilgilerini Giriniz");
                    string search = Console.ReadLine();
                    info.clear();
                    List<Info> Searchresult = info.Search(search);

                    if (Searchresult.Count == 0)
                    {
                        Console.WriteLine("Kayıt Bulunamadı");
                        info.waiting();
                        info.clear();
                    }
                    else
                    {
                        info.Listcontrol(Searchresult);
                        info.Space();
                    }



                }
                else if (key == 4)
                {
                    info.Listcontrol(info.Printlist());
                    info.Space();
                    Console.WriteLine("Silmek İstediğiniz Kaydın Id'sini Giriniz");
                    int id = Convert.ToInt32(Console.ReadLine());
                    int i = 0;
                    foreach (var item in Info.list.ToList())
                    {

                        if (id == item.Id)
                        {
                            info.remove(item);
                            Console.WriteLine("Kayıt Başarıyla Silinmiştir.Anamenüye Yönlendiriliyorsunuz..");
                            info.waiting();
                            info.Listcontrol(info.Printlist());
                            info.Space();
                            break;

                        }
                        else if (id != item.Id && i == Info.list.Count - 1)
                        {
                            info.clear();
                            Console.WriteLine("Aradığınız Id Değerine Sahip Bir Kayıt Bulunamamıştır");
                            info.waiting();
                            info.clear();
                        }
                        i++;

                    }


                }
                else if (key == 0)
                {
                    info.systemexit();
                }
                else
                {
                    info.Fault();
                }

            }

        }

    }

    interface IPersonInfo
    {
        string Name { get; set; }
        string Surname { get; set; }
        string PhoneNumber { get; set; }

    }

    interface Imethods
    {
        List<Info> Printlist();
        int Add();
        List<Info> Search(string search);
        void Space();
        void clear();
        void Backmenu();
        void systemexit();
        void remove(Info info);
    }

    public class Info : IPersonInfo, Imethods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }

        public static List<Info> list = new List<Info>()
            {
              new Info()
              {
                  Id = 1,
                  Name="Ali",
                  Surname="Durmaz",
                  PhoneNumber="05351111111"
              },
              new Info()
              {
                  Id=2,
                  Name="Ali",
                  Surname="Atabak",
                  PhoneNumber="05352222222"
              },
              new Info()
              {
                  Id=3,
                  Name="Aysel",
                  Surname="Kuru",
                  PhoneNumber="05353333333"
              },
              new Info(){
                  Id=4,
                  Name="Zehra",
                  Surname="İşçi",
                  PhoneNumber="05354444444"

              }
          };

        public List<Info> Printlist()
        {
            return list;
        }
        public int Add()
        {
            int a = 0;
            Info info = new Info();

            while (true)
            {
                Space();
                Console.WriteLine("İsim Giriniz");
                Name = Console.ReadLine();
                Console.WriteLine("Soyisim Giriniz");
                Surname = Console.ReadLine();

                foreach (var item in list)
                {
                    if (Name + Surname == (item.Name + item.Surname).ToLower())
                    {
                        Space();
                        Console.WriteLine("Bu İsim ve Soyisim Daha Önce Kayıt Edilmiştir." +
                            "\nYeniden Denemek İçin 1 \nÜzerine Yazdırmak için 2" +
                            "\nAnamenü İçin 0");
                        int key = Convert.ToInt32(Console.ReadLine());
                        if (key == 1)
                        {
                            clear();
                            a = -1;
                            continue;
                        }
                        else if (key == 2)
                        {
                            remove(item);
                            a = 1;
                            break;
                        }
                        else if (key == 0)
                        {
                            a = 0;
                            break;
                        }
                        else
                        {
                            clear();
                            Fault();
                            a = 0;
                            continue;
                        }

                    }
                    else
                    {
                        a = 1;
                    }


                }
                if (a == 0 || a == 1)
                {
                    break;
                }

            }

            if (a == 0)
            {
                return a;
            }
            else
            {
                info.Name = Name;
                info.Surname = Surname;
                while (true)
                {
                    Console.WriteLine("Numara Giriniz");
                    PhoneNumber = Console.ReadLine();
                    foreach (var item in list)
                    {
                        if (PhoneNumber == item.PhoneNumber)
                        {
                            Console.WriteLine("Bu Numaraya Kayıtlı Bir Kişi Bulunmaktadır." +
                                "\nAnamenüye Dönmek İçin 0 \nYeniden Denemek İçin 1");
                            int key = Convert.ToInt32(Console.ReadLine());
                            if (key == 0)
                            {
                                a = 0;
                                break;
                            }
                            else if (key == 1)
                            {
                                a = -1;
                            }
                            else
                            {
                                clear();
                                Fault();
                                a = 0;
                                break;

                            }
                        }
                        else
                        {
                            a = 1;

                        }

                    }

                    bool IsLength = true;
                    bool IsNum = true;

                    foreach (char item in PhoneNumber)
                    {
                        if (!Char.IsNumber(item))
                        {
                            IsNum = false;
                            break;
                        }


                    }
                    if (PhoneNumber.Length != 11)
                    {
                        IsLength = false;

                    }


                    if (IsNum == false && IsLength == false)
                    {
                        clear();
                        Console.WriteLine("Numara Sadece Rakam İçermelidir ve 11 Karakterden Küçük Olamaz.Lütfen Tekrar Deneyiniz.");
                        Space();
                        a = -1;
                    }
                    else if (IsNum == false && IsLength == true)
                    {
                        clear();
                        Console.WriteLine("Numara Sadece Rakam İçermelidir.Lütfen Tekrar Deneyiniz.");
                        Space();
                        a = -1;
                    }
                    else if (IsNum == true && IsLength == false)
                    {
                        clear();
                        Console.WriteLine("Numara 11 Karakterden Küçük Olamaz.Lütfen Tekrar Deneyiniz.");
                        Space();
                        a = -1;
                    }



                    if (a == 0 || a == 1)
                    {
                        break;
                    }

                }

                if (a == 1)
                {
                    info.Id = list[list.Count - 1].Id + 1;
                    info.PhoneNumber = PhoneNumber;
                    list.Add(info);
                    return a;
                }
                else
                {
                    return a;
                }

            }

        }
        public List<Info> Search(string search)
        {
            List<Info> Findresult = new List<Info>();

            foreach (var item in list)
            {
                if ((item.Name + item.Surname + item.PhoneNumber).ToLower().Contains(search.Trim().Replace(" ", String.Empty).ToLower()))
                {
                    Findresult.Add(item);
                }
            }

            return Findresult;
        }
        public void Space()
        {
            Console.WriteLine();
        }
        public void clear()
        {
            Console.Clear();
        }
        public void Fault()
        {
            Console.WriteLine("Hatalı Bir Tuşlama Yaptınız.Anamenüye Yönlendiriliyorsunuz..");
            Space();
            waiting();
            clear();
        }
        public void waiting()
        {
            Thread.Sleep(2000);
        }
        public void Listcontrol(List<Info> list)
        {
            if (list.Count == 0)
            {
                clear();
                Console.WriteLine("Kayıt Bulunamamıştır.Anamenüye Yönlendiriliyorsunuz..");
                waiting();
                clear();
            }
            else
            {
                clear();

                int i = 1;

                foreach (var item in list.OrderBy(x => x.Name + x.Surname))
                {
                    Console.WriteLine(i + "-" + item.Name + " " + item.Surname + " " + "Telefon Numarası:" + " " + item.PhoneNumber + " " + "Id:" + " " + item.Id);

                    i++;

                }
            }

        }
        public void systemexit()
        {
            clear();
            Console.WriteLine("Sistemden Çıkıyorsunuz..");
            waiting();
            Environment.Exit(2000);
        }
        public void remove(Info info)
        {
            list.Remove(info);
        }
        public void Backmenu()
        {

            Space();
            Console.WriteLine("Anamenüye Dönmek İçin 1 \nÇıkmak İçin 0");
            int key2 = Convert.ToInt32(Console.ReadLine());
            if (key2 == 1)
            {
                clear();
            }
            else if (key2 == 0)
            {
                systemexit();
            }
            else
            {
                Fault();
            }
        }

    }

}

