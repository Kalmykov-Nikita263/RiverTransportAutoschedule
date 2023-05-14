using System.ComponentModel.DataAnnotations;

namespace RiverTransportAutoschedule.Domain.Entities;

public enum RiverTransportType
{
    [Display(Name = "Грузовое")]
    Cargo,

    [Display(Name = "Пассажирское")]
    Passenger,

    [Display(Name = "Баржа")]
    Barge
}
