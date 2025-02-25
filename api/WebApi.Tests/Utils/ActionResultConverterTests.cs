using Common.Data;
using Microsoft.AspNetCore.Mvc;
using WebApi.Utils;

namespace WebApi.Tests.Utils;

public class ActionResultConverterTests
{
    [Test]
    [TestCase(FeatureResultStatus.Ok, typeof(OkObjectResult))]
    [TestCase(FeatureResultStatus.NotFound, typeof(NotFoundObjectResult))]
    [TestCase(FeatureResultStatus.Error, typeof(ObjectResult))]
    public void ToActionResultReturnsCorrectType(FeatureResultStatus status, Type expectedType)
    {
        var featureResult = FeatureResult<string>.Ok("");
        featureResult.Status = status;
        var result = featureResult.ToActionResult();
        Assert.IsInstanceOf(expectedType, result);
    }
}