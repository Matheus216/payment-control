using System.Text.Json.Serialization;

namespace payment_control_application.Models;

public class Result<TResult>
{
    public bool Success { get; private set; }
    public TResult? Data { get; set; }
    public IEnumerable<string> ErrorMessages { get; set; }

    [JsonIgnore]
    public CodReturn CodReturn { get; private set; }

    public Result(TResult data, CodReturn codReturn = CodReturn.Ok)
    {
        this.Data = data;
        this.Success = true;
        this.CodReturn = codReturn;
        this.ErrorMessages = new List<string>();

    }

    public Result(IEnumerable<string> errorMessages, CodReturn codReturn = CodReturn.InternalServerError)
    {
        this.ErrorMessages = errorMessages;
        this.Success = false;
        this.CodReturn = codReturn;
    }

    public Result(string errorMessage, CodReturn codReturn = CodReturn.InternalServerError)
    {
        this.ErrorMessages = new List<string> { errorMessage };
        this.Success = false;
        this.CodReturn = codReturn;
    }
}

public enum CodReturn
{
    None = 0,
    NotFound = 404,
    BadRequest = 400,
    InternalServerError = 500,
    Ok = 200,
    Created = 201
}