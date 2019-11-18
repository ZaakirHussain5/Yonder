using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Yonder.Models;

namespace Yonder.ViewModels
{
    public class HomePageViewModel
    {
        public List<SliderImage> SliderImages { get; set; }

        public WebContent WhatsNewContent { get; set; }

        public List<Testimonials> Testimonials { get; set; }
    }
}