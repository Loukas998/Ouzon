using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Domain.Entities.ResponseEntity;

public class BaseResponse
{
    public bool SuccessStatus { get; set; }
    public List<string>? Errors { get; set; }

    public BaseResponse Success()
    {
        this.SuccessStatus = true;
        this.Errors = new List<string>();
        return this;
    }

}
public class BaseResponse<T> : BaseResponse
{
    public T? Data { get; set; }
    public BaseResponse Success(T data)
    {
        this.Data = data;
        this.SuccessStatus = true;
        this.Errors = new List<string>();
        return this;
    }
}
