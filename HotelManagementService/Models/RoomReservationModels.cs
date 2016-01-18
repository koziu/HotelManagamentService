using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HotelManagementService.Enums;

namespace HotelManagementService.Models
{
  [Table("RoomReservation")]
  public class RoomReservationModels
  {
    [Key]
    public Guid Id { get; set; }

    [Display(Name = "Status")]
    public RoomStates RoomState { get; set; }

    public virtual Event Event { get; set; }

    public virtual RoomModels Room { get; set; }
  }
}