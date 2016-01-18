using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.SqlServer;
using HotelManagementService.Enums;

namespace HotelManagementService.Models
{
  public class Event
  {
    //public Event()
    //{
    //  Reservation = new HashSet<ReservationModels>();
    //}
    [Key]
    public Guid Id { get; set; }

    [Display(Name = "Pokój")]
    [Required(ErrorMessage = "Musisz wybrać pokój")]
    public virtual RoomModels Room { get; set; }

    [Display(Name = "Cena")]
    [Required(ErrorMessage = "Musisz podać cene")]
    public double Price { get; 
      set ; }

    [Display(Name = "Status")]
    [Required(ErrorMessage = "Musisz wybrać status")]
    public virtual ReservationStates ReservationState { get; set; }

    [Display(Name = "Data przyjazdu")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Required(ErrorMessage = "Musisz podać datę przyjazu")]
    [DataType(DataType.Date)]
    public DateTime ArriveDate { get; set; }

    [Display(Name = "Data wyjazdu")]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    [Required(ErrorMessage = "Musisz podać datę wyjazdu")]
    [DataType(DataType.Date)]
    public DateTime DepatureDate { get; set; }

    public RoomStates RoomState { get; set; }

    public virtual ReservationModels Reservation { get; set; }
  }
}