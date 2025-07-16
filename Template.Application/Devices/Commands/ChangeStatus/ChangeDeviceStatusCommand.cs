using MediatR;
namespace Template.Application.Devices.Commands.ChangeStatus;

public class ChangeDeviceStatusCommand : IRequest
{
    public int DeviceId { get; set; }
    public bool OptIn { get; set; }
}
