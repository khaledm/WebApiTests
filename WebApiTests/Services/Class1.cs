using System;
using System.Collections.Generic;

namespace WebApiTests.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IResponseObjectBuilder<T> CreateNew<T>()
        {
            return new ResponseObjectBuilder<T>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseObjectBuilder<T> : IResponseObjectBuilder<T>
    {
        private readonly List<MulticastDelegate> _functions = new List<MulticastDelegate>();

        public T Build(List<MulticastDelegate> defaDelegates)
        {
            var obj = Construct(defaDelegates);
            CallFunctions(obj);

            return obj;
        }

        public T Build()
        {
            var obj = Construct();
            CallFunctions(obj);

            return obj;
        }

        private void CallFunctions(T obj)
        {
            foreach (var propDelegate in _functions)
            {
                propDelegate.DynamicInvoke(obj);
            }
        }

        private T Construct()
        {
            try
            {
                return (T)Activator.CreateInstance(typeof(T), true);
            }
            catch (MissingMethodException e)
            {
                throw new ApplicationException(typeof(T).Name + " does not have a default parameterless constructor", e);
            }
        }

        private T Construct(List<MulticastDelegate> defaultProps)
        {
            try
            {
                var obj = (T) Activator.CreateInstance(typeof(T), true);
                defaultProps.ForEach( del => del.DynamicInvoke(obj));
                return obj;
            }
            catch (MissingMethodException e)
            {
                throw new ApplicationException(typeof(T).Name + " does not have a default parameterless constructor", e);
            }
        }

        public IResponseObjectBuilder<T> SetPropertyWith<TFunc>(Func<T, TFunc> func)
        {
            _functions.Add(func);
            return this;
        }
    }

    public interface IResponseObjectBuilder<T> : IBuildable<T>
    {
        IResponseObjectBuilder<T> SetPropertyWith<TFunc>(Func<T, TFunc> func);
    }

    public interface IBuildable<T>
    {
        T Build();
    }
}