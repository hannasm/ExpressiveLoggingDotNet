using System;

namespace ExpressiveLogging.V3.Tests {
	namespace MyLibrary {
		public partial class MyComponent {
			static readonly ILogToken _lt = LogManager.GetToken(); // # Name = MyLibrary.MyComponent
			static readonly ILogToken _sclt = LogManager.GetToken(_lt, "SubComponent"); // # Name = MyLibrary.MyComponent.SubComponent
			static readonly ILogToken _dynlt = LogManager.GetToken("DynamicComponent"); // # Name = DynamicComponent
		}
	}

	namespace MyLibrary {
		public partial class MyComponent {
			public void ErrorChannelLoggingExamples01() {
				var log = LogManager.Logs;
																																																							
				log.Error(_lt, m=>m(new Exception("An error ocurred"))); // exception only
				log.Error(_lt, m=>m(new Exception("An error ocurred"), "Format String")); // exception and raw string
				log.Error(_lt, m=>m(new Exception("An error ocurred"), "Format String {0} - {1}", DateTime.Now, 20)); // exception and format string
																																																							
				log.Error(_lt, m=>m(new Exception("An error ocurred"), -1)); // exception plus uniqueness code
				log.Error(_lt, m=>m(new Exception("An error ocurred"), -1, "Format String")); // exception, uniqueness code, and raw string
				log.Error(_lt, m=>m(new Exception("An error ocurred"), -1, "Format String {0} - {1}", DateTime.Now, 20)); // exception, uniqueness code, and format string
																																																							
				log.Error(_lt, m=>m("Format String")); // uniqueness code, and raw string
				log.Error(_lt, m=>m("Format String {0} - {1}", DateTime.Now, 20)); // uniqueness code, and format string
																																																							
				log.Error(_lt, m=>m(-1, "Format String")); // uniqueness code, and raw string
				log.Error(_lt, m=>m(-1, "Format String {0} - {1}", DateTime.Now, 20)); // uniqueness code, and format string
			}
		}
	}

	namespace MyLibrary {
		public partial class MyComponent {
			public void ErrorChannelLoggingExamples02() {
				var log = LogManager.Error;
																																																							
				log.Write(_lt, m=>m(new Exception("An error ocurred"))); // exception only
				log.Write(_lt, m=>m(new Exception("An error ocurred"), "Format String")); // exception and raw string
				log.Write(_lt, m=>m(new Exception("An error ocurred"), "Format String {0} - {1}", DateTime.Now, 20)); // exception and format string
																																																							
				log.Write(_lt, m=>m(new Exception("An error ocurred"), -1)); // exception plus uniqueness code
				log.Write(_lt, m=>m(new Exception("An error ocurred"), -1, "Format String")); // exception, uniqueness code, and raw string
				log.Write(_lt, m=>m(new Exception("An error ocurred"), -1, "Format String {0} - {1}", DateTime.Now, 20)); // exception, uniqueness code, and format string
																																																							
				log.Write(_lt, m=>m("Format String")); // uniqueness code, and raw string
				log.Write(_lt, m=>m("Format String {0} - {1}", DateTime.Now, 20)); // uniqueness code, and format string
																																																							
				log.Write(_lt, m=>m(-1, "Format String")); // uniqueness code, and raw string
				log.Write(_lt, m=>m(-1, "Format String {0} - {1}", DateTime.Now, 20)); // uniqueness code, and format string
			}
		}
	}

	namespace MyLibrary {
		public partial class MyComponent {
			public void EachChannelLoggingExample() {
				var log = LogManager.Logs;
																																																							
				log.Alert(_lt, m=>m("Alert message"));
																																																							
				log.Error(_lt, m=>m("Error message"));
																																																							
				log.Warning(_lt, m=>m("Warning message"));
																																																							
				log.Audit(_lt, m=>m("Audit message"));
																																																							
				log.Info(_lt, m=>m("Info message"));
																																																							
				log.Debug(_lt, m=>m("Debug message"));
			}
		}
	}

	namespace MyLibrary {
		public partial class MyComponent {
			static readonly ICounterToken _totalRequests = LogManager.CreateRawCounterToken("TotalRequests");
			static readonly ICounterToken _activeRequests = LogManager.CreateRawCounterToken("ActiveRequests");

      static readonly ILogToken _databaseLayer = LogManager.GetToken("DatabaseLayer");
      static readonly ILogToken _businessLayer = LogManager.GetToken("BusinessLayer");
			static readonly INamedCounterToken _processTimeDB = LogManager.CreateNamedCounterToken("ProcessTime", _databaseLayer);
			static readonly INamedCounterToken _processTimeBL = LogManager.CreateNamedCounterToken("ProcessTime", _businessLayer);
		}
	}

	namespace MyLibrary {
		using System.Diagnostics;
		public partial class MyComponent {
			public void CounterExample(ILogStream log) {
				log.IncrementCounter(_totalRequests);
				log.IncrementCounter(_activeRequests);
				Stopwatch timer = Stopwatch.StartNew();
																																																							
				// do additional work here
																																																							
				SubComponentCounterExample(log);
																																																							
				// do additional work here
																																																							
				log.IncrementCounterBy(_processTimeBL, timer.ElapsedMilliseconds);
				log.DecrementCounter(_activeRequests);
			}
			public void SubComponentCounterExample(ILogStream log) {
				Stopwatch timer = Stopwatch.StartNew();
																																																							
				// do additional work here
																																																							
				log.IncrementCounterBy(_processTimeDB, timer.ElapsedMilliseconds);
			}
		}
	}

	namespace MyLibrary {
		public partial class MyComponent {
			public void SimpleScopedExample(IExpressiveLogs log) {
				using (LogManager.NewScope(_lt))
				{
					// some behavior takes place here
																																																							
				}
																																																							
			}
		}
	}
	namespace MyLibrary {
		public partial class MyComponent {
			public void ContextCaptureExample(IExpressiveLogs log) {
				using (LogManager.BuildScope(_dynlt).
					AddContext("Tracking", Guid.NewGuid().ToString("N")).
					AddContext("ThreadID", System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()).
					NewScope()
				)
				{
					// some behavior takes place here
				}
			}
		}
	}
	namespace MyLibrary {                                                                                       
		using System.Collections.Generic;
		public partial class MyComponent {                                                                        
			public static void SetupMessagingScopedActionStream() {                                                 
				var auditProxy = StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_AUDIT);                         
																																																							
				// NOTE: For best performance, only use a single scoped action for all scoped behaviors, and inject it somewhere appropriate in the middle of your stack instead of into the proxy                                
				auditProxy.Add(new ScopedActionLogStream(                                                             
					(lt, parameters)=>LogManager.Logs.Audit(lt, m=>m("Create log scope in {0}", lt.Name)),              
					(lt, parameters)=>LogManager.Logs.Audit(lt, m=>m("Terminate log scope in {0}", lt.Name)),           
					new NullLogStream()                                                                                 
				));                                                                                                    
			}                                                                                                       
			public static void SetupContextModifyingScopedActionStream() {                                          
				var auditProxy = StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_AUDIT);                         
																																																							
				// NOTE: For best performance, only use a single scoped action for all scoped behaviors, and inject it somewhere appropriate in the middle of your stack instead of into the proxy                                
				auditProxy.Add(new ScopedActionLogStream(                                                             
					(lt, parameters)=>{                                                                                 
						parameters.Add(new KeyValuePair<string,object>("ScopeCreationTime", DateTime.Now.ToString()));                                     
					},                                                                                                  
					(lt, parameters)=>{},                                                                               
					new NullLogStream()                                                                                 
				));                                                                                                    
			}                                                                                                       
		}                                                                                                         
	}
	namespace MyLibrary {
		public partial class ApplicationLogInitializer {
			public static void InitializeLogging() {
				StaticLogRepository.Init(ApplicationLogInitializer.LogFactoryBuilder);
				StaticLogRepository.SetTokenTransform(ApplicationLogInitializer.TransformTokens);
			}
			public static ILogStreamToken TransformTokens(ILogStreamToken tok) {
				switch (tok.Name) {
					case LogManager.STREAM_TOKEN_ALERT_NAME:
					case LogManager.STREAM_TOKEN_ERROR_NAME:
					case LogManager.STREAM_TOKEN_WARNING_NAME:
					case LogManager.STREAM_TOKEN_AUDIT_NAME:
					case LogManager.STREAM_TOKEN_INFO_NAME:
					case LogManager.STREAM_TOKEN_DEBUG_NAME:
						return tok;
					default: return LogManager.STREAM_TOKEN_WARNING;
				}

			}
			public static Func<ProxyLogStream, ILogStream> LogFactoryBuilder(ILogStreamToken tok) {
				switch (tok.Name) {
					case LogManager.STREAM_TOKEN_ALERT_NAME: return ApplicationLogInitializer.BuildAlertStream;
					case LogManager.STREAM_TOKEN_ERROR_NAME: return ApplicationLogInitializer.BuildErrorStream;
					case LogManager.STREAM_TOKEN_WARNING_NAME: return ApplicationLogInitializer.BuildWarningStream;
					case LogManager.STREAM_TOKEN_AUDIT_NAME: return ApplicationLogInitializer.BuildAuditStream;
					case LogManager.STREAM_TOKEN_INFO_NAME: return ApplicationLogInitializer.BuildInfoStream;
					case LogManager.STREAM_TOKEN_DEBUG_NAME: return ApplicationLogInitializer.BuildDebugStream;

					// this will never run because we change any other token we see into a WARNING with TransformTokens()
					default: return ApplicationLogInitializer.BuildOtherStreams;
				}
			}
			public static ILogStream BuildOtherStreams(ProxyLogStream stream) {
				return BuildWarningStream(stream);
			}
			public static ILogStream BuildStandardStack(ProxyLogStream stream) {
				return new ExceptionHandlingLogStream(
					new UniquenessCodeGeneratorLogStream(
						new EmptyFormatMessageFixer(
							new ExceptionFormatterLogStream(
								new DefaultTextLogStreamFormatter(
									stream
				)))));
			}
			public static ILogStream BuildAlertStream(ProxyLogStream stream) {
				stream.Add(new StderrLogStream());
				return BuildStandardStack(stream);
			}
			public static ILogStream BuildErrorStream(ProxyLogStream stream) {
				stream.Add(new StderrLogStream());
				return BuildStandardStack(stream);
			}
			public static ILogStream BuildWarningStream(ProxyLogStream stream) {
				stream.Add(new StderrLogStream());
				return BuildStandardStack(stream);
			}
			public static ILogStream BuildAuditStream(ProxyLogStream stream) {
				stream.Add(new StdoutLogStream());
				return BuildStandardStack(stream);
			}
			public static ILogStream BuildInfoStream(ProxyLogStream stream) {
				return new NullLogStream();
			}
			public static ILogStream BuildDebugStream(ProxyLogStream stream) {
				return new NullLogStream();
			}
		}
	}
}
