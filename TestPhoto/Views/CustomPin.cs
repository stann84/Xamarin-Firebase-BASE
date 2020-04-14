using System;
using Xamarin.Forms.Maps;

public class CustomPin
{
    public CustomPin()
    {
        MapPin = new Pin();
    }
    public string Id { get; set; }
    public EventHandler Url { get; set; }
    public Pin MapPin { get; set; }
}