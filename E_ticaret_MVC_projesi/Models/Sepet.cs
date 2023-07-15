using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_ticaret_MVC_projesi.Models
{

    public class Sepet
    {  
        private List<Sepetlik> _sepetim = new List<Sepetlik>();
        public List<Sepetlik> Sepetim { get => _sepetim; }
        public void Sepete_ekle(urunler gelen_urun,byte adet)
        {
            var varolan_urun = _sepetim.FirstOrDefault(x => x.urun.urunid == gelen_urun.urunid);
            if (varolan_urun == null)//sepette bu ürün yok yani ilk kez eklenecek
            {
                _sepetim.Add(new Sepetlik { urun = gelen_urun, adet = 1 });
            }
            else if (adet == 0) varolan_urun.adet += 1;//tekrar linkle ürün seçilir ve  gelinirse 
            else varolan_urun.adet = adet;//adet kutusunda artım azalalım yapıp sepet sayfası formundan gelinirse
        }
        public void Sepeten_sil(urunler silinecek_urun)
        {
            _sepetim.RemoveAll(x=>x.urun.urunid==silinecek_urun.urunid);
        }

        public double Sepet_toplami()
        {
            return _sepetim.Sum(x => x.urun.fiyat * x.adet);
        }
        public void Sepeti_temizle()
        {
            _sepetim.Clear();
        }
    }
}