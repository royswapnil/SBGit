using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace SterlingBankLMS.Web.Utilities.Extensions
{
    public static class MappingExtensions
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return Startup.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination) //where TSource : BaseValidatableVM
        {
            return Startup.Mapper.Map(source, destination);
        }

        public static IMappingExpression<TSource, TDestination> MapProperty<TSource, TDestination>(
           this IMappingExpression<TSource,
           TDestination> map,
           Expression<Func<TDestination, object>> destination, Expression<Func<TSource, object>> source)
        {

            map.ForMember(destination, config => config.MapFrom(source));

            return map;
        }


        public static IMappingExpression<TSource, TDestination> IgnoreProperties<TSource, TDestination>(
           this IMappingExpression<TSource,
           TDestination> map,
          params Expression<Func<TDestination, object>>[] selectors)
        {

            foreach (var selector in selectors) {
                map.ForMember(selector, config => config.Ignore());
            };
            return map;
        }

        public static IQueryable<T> ProjectToType<T>(this IQueryable source)
        {
            return source.ProjectTo<T>(Startup.Mapper.ConfigurationProvider);
        }
    }
}