using Common.Data;
using UseCases.Spinning;

namespace WebApi.RequestModels;

public class WheelSpinRequest
{
    public WheelSetting WheelSetting { get; set; }
    public WheelSpinOptions? Options { get; set; }
}