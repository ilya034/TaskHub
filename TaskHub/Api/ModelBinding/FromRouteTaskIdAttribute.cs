using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.ModelBinding;

public sealed class FromRouteTaskIdAttribute : ModelBinderAttribute
{
    public FromRouteTaskIdAttribute() : base(typeof(FromRouteTaskIdModelBinder))
    {
        BindingSource = BindingSource.Path;
        Name = "id";
    }
}
