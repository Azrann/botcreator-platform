using System;
using System.Collections.Generic;
using System.Linq;
using Bot.telegram.funnel.Models;
using Newtonsoft.Json;
using Xunit;

namespace Mentor.telegram.Bot.test
{
    public class SlidesTest
    {
        [Fact]
        public void ICanCallToMessageAnEmptySlidesObject()
        {
            Slides slides = GetEmptySlides();
            var jsonSlide = slides.ToMessageJson();
           
            Assert.False(string.IsNullOrEmpty(jsonSlide));
        }

        [Fact]
        public void ICanCallToMessageSlides()
        {
            Slides slides = GetSimpleSlides();
            var jsonSlide = slides.ToMessageJson();

            Assert.False(string.IsNullOrEmpty(jsonSlide));
        }


        [Fact]
        public void ICanSerializeDeserializeSlides()
        {
            Slides slides = GetSimpleSlides();
            var jsonSlide = JsonConvert.SerializeObject(slides);

            Slides result = JsonConvert.DeserializeObject<Slides>(jsonSlide);

            Assert.True(result.Data.Count == 2);
            Assert.True(result.Data[0].Buttons.Count == 2);
            Assert.True(result.Data.First().Description== "The best marketing tool");
            Assert.True(result.Data[1].Title == "Programming lessons");
            Assert.True(result.Data[1].ImageUrl == "https://www.probytes.net/wp-content/uploads/2018/01/4-1.png");
        }

        private Slides GetEmptySlides()
        {
            var result = new Slides {Data = new List<Slide>()};
            return result;
        }

        private Slides GetSimpleSlides()
        {
            var result = new Slides
            {
                Data = new List<Slide>()
                {
                    new Slide()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Buttons = new List<string>(){"Continue", "Cancel"},
                        Description = "The best marketing tool",
                        ImageUrl = "https://www.timecamp.com/blog/wp-content/uploads/2018/02/work-memes-1.jpg",
                        Title = "Important lessons"
                    },
                    new Slide()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Buttons = new List<string>(){"Buy", "Cancel"},
                        Description = "Wise meme about nothing",
                        ImageUrl = "https://www.probytes.net/wp-content/uploads/2018/01/4-1.png",
                        Title = "Programming lessons"
                    }
                }
            };
            return result;
        }
    }
}
