using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.HTTP.ProcessingPipeline {
	public static class HttpEventBus {
		//static Dictionary<Type,Dictionary<Type,Action<object,object>>> Handlers = new Dictionary<Type,Dictionary<Type,Action<object,object>>>();
		//static object Lock = new object();
		//public static void Register<THandler,TEvent>(Action<THandler,TEvent> eventHandler) where TEvent :class where THandler:class{
		//	//lock(Lock) {
		//	//	var eventType = typeof(TEvent);
		//	//	if(!Handlers.ContainsKey(eventType)) {
		//	//		Handlers[eventType] = new Dictionary<Type,Action<object,object>>();
		//	//	}
				

		//	//	Handlers[eventType][typeof(THandler)]= CastArgument<object,TEvent,object,THandler>((h,e)=>eventHandler(h,e)));
		//	//}
		//}

		public static void Dispatch<T>(HttpProcessingPipelineContext context, object[] handlers, T @event) where T:class { 	
			//var type = typeof(T);
			//if(!Handlers.ContainsKey(type)) {
			//	return;
			//}
			////TODO: make sure we can't register after first call to Dispatch
			//foreach(var handler in Handlers[type]) {
			//	handler(@event);
			//}
			foreach(var handler in handlers.OfType<IHandleHttpProcessingEvents<T>>().Cast<IHandleHttpProcessingEvents<T>>()) {
				handler.Handle(context,@event);
			}
		}

		//from http://codebetter.com/gregyoung/2009/10/03/delegate-mapper/
		//static Action<BaseEventT,BaseHandlerT> CastArgument<BaseEventT,DerivedEventT, BaseHandlerT,DerivedHandlerT>
		//	(Expression<Action<DerivedHandlerT,DerivedEventT>> source) where DerivedEventT:BaseEventT where DerivedHandlerT : BaseHandlerT {
		//	if(typeof(DerivedEventT) == typeof(BaseEventT) && typeof(BaseHandlerT)== typeof(DerivedHandlerT)) {
		//		return (Action<BaseEventT,BaseHandlerT>)((Delegate)source.Compile());

		//	}
		//	ParameterExpression sourceEventParameter = Expression.Parameter(typeof(BaseEventT),"sourceEvent");
		//	ParameterExpression sourceHandlerParameter = Expression.Parameter(typeof(BaseHandlerT),"sourceHandler");
		//	var result = Expression.Lambda<Action<BaseEventT,BaseHandlerT>>(
		//		Expression.Invoke(
		//			source,
		//			Expression.Convert(sourceHandlerParameter,typeof(DerivedHandlerT)),
		//			Expression.Convert(sourceEventParameter,typeof(DerivedEventT))
					
		//			),
		//		sourceEventParameter);
		//	return result.Compile();
		//}
	}
}
