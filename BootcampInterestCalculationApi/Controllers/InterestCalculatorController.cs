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
        public double Balance { get; set; }

        //Faiz oranı property'si sayısal değer ve boş geçilemez olduğunu ifade eden validasyon
        [Required(ErrorMessage = "Please enter number ")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid number")]
        public double InterestAmount { get; set; }

        //Vade süresi property'si sayısal değer ve boş geçilemez olduğunu ifade eden validasyon
        [Required(ErrorMessage = "Please enter number ")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid number")]
        public double Time { get; set; }
    }
    //Post edilen verilerin sonucu oluşan faiz sonucunu ve giriş değişkenlerini veren sınıf.
    public class InterestResult
    {
        //Faiz oranı
        public double InterestAmount { get; set; }

        //Faiz sonu anapara
        public double TotalBalance { get; set; }
    }

    [ApiController]
    [Route("Calculations")]
    public class InterestCalculatorController : ControllerBase
    {
        //Get metodu ile hesaplama işlemi
        [HttpGet("CalculationGetMethod")]
        public ActionResult<InterestResult> CalculationGetMethod([FromQuery] InterestValue interestValue)
        {
            //Sınıf newlenerek içindeki property'lere erişilir.
            InterestResult interestResult = new();

            //Girilen faiz oranının sonuç sınıfındaki property'e eşitlenmesi.       
            interestResult.InterestAmount = interestValue.InterestAmount;

            //Vade sonucunun sonuç sınıfındaki property'e eşitlenmesi.   
            interestResult.TotalBalance = interestValue.Balance * Math.Pow((1 + (interestValue.InterestAmount / 100)), interestValue.Time);

            //Sonuç sınıfının response edilmesi.
            return interestResult;
        }
    }
}
