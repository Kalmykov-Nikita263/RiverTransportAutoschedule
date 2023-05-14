using System.ComponentModel.DataAnnotations;

namespace RiverTransportAutoschedule.Infrastructure;

public enum WeatherType
{
    [Display(Name = "Дождь")]
    Rainy,

    [Display(Name = "Туман")]
    Foggy,

    [Display(Name = "Благоприятно")]
    Good
}