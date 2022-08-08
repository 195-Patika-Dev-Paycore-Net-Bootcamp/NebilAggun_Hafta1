using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BootcampInterestCalculationApi.Controllers
{
    //API ekranından faiz hesabı için post edilecek değişkenlerin property'leri için oluşturulan sınıf.
    public class InterestValue
    {
        //Ana para property'si sayısal değer ve boş geçilemez olduğunu ifade eden validasyon
        [Required(ErrorMessage = "Please enter number ")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid number")]
        public double Principal { get; set; }

        //Faiz oranı property'si sayısal değer ve boş geçilemez olduğunu ifade eden validasyon
        [Required(ErrorMessage = "Please enter number ")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid number")]
        public double InterestRate { get; set; }

        //Vade süresi property'si sayısal değer ve boş geçilemez olduğunu ifade eden validasyon
        [Required(ErrorMessage = "Please enter number ")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid number")]
        public double Time { get; set; }
    }
    //Post edilen verilerin sonucu oluşan faiz sonucunu ve giriş değişkenlerini veren sınıf.
    public class InterestResult
    {
        //Ana para
        public double Principal { get; set; }

        //Faiz oranı
        public double InterestRate { get; set; }

        //Vade süresi 
        public double Time { get; set; }

        //Faiz sonu anapara
        public double NewPrincipal { get; set; }
    }

    [ApiController]
    [Route("Calculations")]
    public class InterestCalculatorController : ControllerBase
    {
        //Dönüş listesi 
        private List<InterestResult> GetList()
        {
            List<InterestResult> list = new();
            return new List<InterestResult>(list);
        }
        //Get metodu ile hesaplama işlemi
        [HttpGet("CalculationGetMethod")]
        public List<InterestResult> CalculationGetMethod([FromQuery] InterestValue interestValue)
        {
            //Sınıf newlenerek içindeki property'lere erişilir.
            var interestResult = new InterestResult();

            //Girilen faiz oranının sonuç sınıfındaki property'e eşitlenmesi.       
            interestResult.InterestRate = interestValue.InterestRate;

            //Girilen anaparanın sonuç sınıfındaki property'e eşitlenmesi.   
            interestResult.Principal = interestValue.Principal;

            //Girilen vade süresinin sonuç sınıfındaki property'e eşitlenmesi.   
            interestResult.Time = interestValue.Time;

            //Vade sonucunun sonuç sınıfındaki property'e eşitlenmesi.   
            interestResult.NewPrincipal = interestValue.Principal * Math.Pow((1 + (interestValue.InterestRate / 100)), interestValue.Time);

            // list değişkeninin oluşturulası 
            var list = GetList();

            //listeye sonuç property'lerin eklenmesi
            list.Add(interestResult);

            //Sonuç sınıfının eklendiği listenin response edilmesi.
            return new List<InterestResult>(list);
        }
    }
}
