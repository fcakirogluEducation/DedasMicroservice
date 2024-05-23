using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer.App
{
    internal record UserCreatedEventV2(int Id, string Email, int Phone, string Key);

    // usercreatedEvent1==usercreatedEvent2
    //internal class UserCreatedEvent
    //{
    //    public int Id { get; init; }
    //    public string Email { get; init; } = default!;
    //    public string Phone { get; init; } = default!;
    //}
}