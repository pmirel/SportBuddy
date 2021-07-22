using System;
using System.Collections.Generic;

namespace API.DTOs
{
  public class MemberDto
  {
    public int Id { get; set; }
    public string Username { get; set; }
    public string PhotoUrl { get; set; }

    public int Age { get; set; }
    public string NickName { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastActive { get; set; }
    public string Gender { get; set; }
    public string Sport { get; set; }
    public int Experience { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public ICollection<PhotoDto> Photos { get; set; }
  }
}