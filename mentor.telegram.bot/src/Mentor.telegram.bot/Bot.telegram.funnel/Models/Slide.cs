using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Bot.telegram.funnel.Models
{
    public class Slide
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Buttons { get; set; }
    }

    public class Slides
    {
        public List<Slide> Data { get; set; }

        public string ToMessageJson()
        {
            StringBuilder result = new StringBuilder("{\n");
            for (int i = 0; i < Data.Count; i++)
            {
                var slide = Data[i];
                result.AppendLine($"slide_{i}: {{");
                result.AppendLine($"id: {slide.Id},");
                result.AppendLine($"image_url: '{slide.ImageUrl}',");
                result.AppendLine($"title: '{slide.Title}',");
                result.AppendLine($"description: '{slide.Description}',");
                result.AppendLine($"buttons: {JsonConvert.SerializeObject(slide.Buttons)}");
                result.AppendLine(i == Data.Count - 1 ? "}" : "},");
            }
            result.Append("\n}");
            return result.ToString();
        }
    }


    //slide_1: {
    //id: 1,
    //image_url: 'https://via.placeholder.com/150',
    //title: 'Slide 1',
    //description: 'this is description for first slide, thanks',
    //buttons: [
    //['Button 1'],
    //['Button 2']
    //]
    //},
    //slide_2: {
    //id: 2,
    //image_url: 'https://via.placeholder.com/150',
    //title: 'Slide 2',
    //description: 'This is description for second slide, thanks',
    //buttons: [
    //['Button 2'],
    //['Button 3']
    //]
    //}


    // JSON CONVERT FORMAT


}
