# Versioning
This is version 3.0.0 of the expressive logging library

The base package is available from nuget at: https://www.nuget.org/packages/ExpressiveLogging/3.0.0

The powershell V5 package is available from nuget at: https://www.nuget.org/packages/ExpressiveLogging.PowershellV5Logging/3.0.0

The source for this release is available on github at: https://github.com/hannasm/ExpressiveLoggingDotNet/releases/tag/3.0.0

# About
The expressive logging project attempts to provide a robust logging framework that leverages lambda expressions to provide
deeply integrated, high performance logging with a deep feature set. The primary advantage offered through the use of 
lambda expressions comes from being able to disable and enable logging behavior at runtime, and achieving a maximal
reduction in runtime overhead when those components are disabled.

Logging is driven by two major components, an ILogStream and ILogToken.

ILogStream provides an API for recording application events. This includes three key pieces
  * Capturing long lived context data, this data is associated with each event as it occurs
  * Capturing performance and system metrics through counters
  * Capturing system behavior through the event system

ILogToken is the identifier which is used to distinguish the source of logging events, and used to uniquely identify Counters
 
ILogStream implementations exist for a variety of different use cases and new ones may created cheaply and easily
to integrate expressive log streams into components that need them, or with any client applications that may
have different logging needs.

ILogStream is composable through the use of a CompositeLogStream. Using composability, it is possible to direct a single application event to multiple destinations.

There are a number of extensions and utilites that enhance the core behavior.
  * The ExpressiveLogging.Filters namespace provides a solution for disabling and enabling events of interest
  * The ExpressiveLogging.StreamFormatters namespace include classes to control output formats, and control for edge cases and other customization

# Log streams
Log streams are just a way to emit events from within an application or component to some other place in a decoupled fashion. The intent for these
particular event streams is to transmit loosely structured messages useful for providing feedback to the user, such as basic textual output or debugging messages.
Each event has an identifier composed of it's log token, uniqueness code, and the message payload (which 
can include an exception, a format string, and an array of elements to inject into the format string.

You can create multiple log streams, and control the flow of events through each of those streams in different ways as needed.

The expressive logging library does not mandate the use of log streams in a specific way, however in order to simplify
and facilitate interoperability between diferent libraries a number of predefined streams are specified.

	* Alert - alerts are the highest importance events, that would warrant immediate attention. These are the ones you are most likely to send via email / sms / etc..
	* Error - this is a slightly lower priority event, that indicates a real issue, but one that might not demand immedaite attention
	* Warning - this is intended for lower priority issues, perhaps for situations that are expected edge cases, and things that may or may not have a clear solution
	* Audit - these are considered non-error related events that are of high priority. Audit events are the logging equivalent of writing to standard output.
	* Info - these are a lower-priority version of audit events, which would only be captured when specifically requested
	* Debug - these are even lower-priority events, which should only be captured during development / debugging scenarios

Log stream payloads consist of a token, an exception, a uniqueness code, and a format string, and the format objects

* The logging token is the highest level classifier of an event, and primarily is used to indicate a component and subsystem from which the event took place.
* The uniqueness code is intended to allow tooling to identify events without parsing. This is generally a hash over the exception / format string / any relevant data that would uniquely identify the event.
* The exception and format data are the highly detailed payload that makes up the remainder of the payload.

# Performance Counters
The performance counter subsystem is a means of visualizing system health and behavior at a higher level of detail. Each performance counter stores an integer, representing a state which will change over time. Making observations of this state gives an immediate realtime view of some particular behavior in the system. Making repeated observations of this state, and using various statistical processes to analyze those values, can lead to surprisingly useful views of a system with minimal overhead.

Recording data to a counter is drastically more efficient for the application, and the consumer of the counter data, when running at high speed and with high volume.

For example, a component handling 1000 requests per second wants to record how long it takes to execute. If that component writes the time of each event to an event stream, that means 1000 events per second are being recorded to disk (or some other medium) and then an administrator needs to sift through each of those 1000 events to asses how the system behaved. In contrast, if this same component adds that time to a counter, and the administrator samples that counter each second. Then the difference between the previous observation and the current is the total amount of time spent processing. If this componenbt also tracks how many requests were processed it is possible to calculate average runtime. 

There are no default implementations to this subsystem at the present time. Contributions would be welcome. This is a future work item.

# Context Tracking
The logging system supports tracking context data. The most obvious form of context might capture things like process ID, thread ID, program name or various correlation ids relevant to the system. Context data is stored in thread local storage, and in such a way that the standard .NET threading model will retain that data during continuations through the thread pool.

Context data can (though usually doesn't) transition across process boundaries, and should only store primitive data types like strings.

ILogStream is deeply integrated into the context management functionality and implementors can both inject custom data into the context, as well as access that context to provide more robust event payloads and log rendering components.

The context data api allows nested contexts, and overlapping nested context data defines a hierarchy. If the same key is found in both a parent and a leaf node, the value from the leaf will win out over the parent. 

All of this is probably way more complicated than most people will need. But if you don't use it it won't cost you anything and if
you do happen to want it you get it for free. 

# Log Stream Repository
The StaticLogRepository is a centralized source for accessing log streams from anywhere inside the application. It ensures that each
thread has exclusive access to it's log stream objects to avoid any thread safety or synchronization concerns. It also provides a common
mediator for components to interact with the logging infrastructure. This componet provides a safe set of defaults so that writing to log streams does not conflict with the primary directive of the code if not needed, and incurs minimal overhead when not being consumed. 

The repository component facilitates the concept of separate streams of logging data. On it's own a stream is a stream, but when there are multiple streams configured in different ways interesting choices can be made about where logging data should be routed. In addition to the built-in logging streams the static log repository facilitates user defined streams.

Generally system code will never interact with the log stream repository, or it will only be accessed indirectly. It's only during a configuration and initialization step that this code will generally be needed.

When you do interact with this component, you'll primarily be thinking about setting up your logging infrastructure, and how your events will be rendered or consumed.

Each log stream is stored in the repository keyed by an ILogStreamToken. 

Each log stream in the repository is made up of two parts, the stack, and then a proxy which represents an output source. The logging stack performs operations on the event stream such as rendering, error handling and other grooming tasks. The proxy is at the bottom of the stack, and acts as an intermediary through which groomed events can be routed to different destinations without requiring more complex reconfiguring of the rest of the grooming process.

The stack has been discussed elsewhere in this docunment in some detail already. 

## Stream Proxying
The proxy is a convention that has proven useful for enabling certain logging use cases. Sometimes it might be desirable to send events to a default destination by default, but also have an option to re-route or multiplex those events at certain stages in the application. Without a proxy the entire stack would need rebuilt each time a change like this was made. While this might technically be feasible in some scenarios, it could also become cumbersome or impossible to implement depending on how the stack has been initialized, and whether the component that is trying to change the destination of the events is able to reliably perform all the steps necesarry to recreate that stack.

The proxy exposed by the static log repository is a placeholder which can be used to control event routing dynamically at runtime. The proxy itself is exposed as a standard component, and is simply being exposed here for the same use cases it might be useful for in other contexts.

It's used by the powershell logging library to multiplex events to both whatever logging configuration the user may have configured themselves, as well as to the powershell console on a temporary basis.

It could be used for example, to send logs to disk by default, but enabling a network transport for realtime remote debugging when requested by the user.

It can be used to enable and disable sending events to a UI thread or any other event sink that might not always be available throughout the application lifecycle, but needs to receive events when it does become available.

# Basic Usage
The first step in doing any logging  is defining some token(s)

```C#
using ExpressiveLogging;

namespace MyLibrary {
	public partial class MyComponent {
		static readonly ILogToken _lt = LogManager.GetToken(); // # Name = MyLibrary.MyComponent
		static readonly ILogToken _sclt = LogManager.GetToken(_lt, "SubComponent"); // # Name = MyLibrary.MyComponent.SubComponent
		static readonly ILogToken _dynlt = LogManager.GetToken("DynamicComponent"); // # Name = DynamicComponent
	}
}
```

Logging tokens can be initialized automatically. The parameterless overload of GetToken() used to initialize _lt uses the stack to derive a token name automatically. In this case 'MyLibrary.MyComponent'.  

Logging tokens can also be derived from an existing logging token. The overload used to initialize _sclt is based on the _lt token, with a period '.' and the word SubComponent added to the end. The final token name would be MyLibrary.MyComponent.SubComponent'.

Logging tokens can also be initialized with custom strings. The overload used to initialize _dynlt creates a logging token with a completely user-defined name.

Logging tokens initialized as static fields have minimal performance / memory cost. Auto initialized tokens created in constructors or inside methods may have a minor performance penalty because of the stack walking work that is required. The derived and custom tokens are less intensive to create.

### Logging Events
In addition to the token, events written to the log stream have an exception, uniqueness code, format string and format data. A uniqueness code will be generated automatically for method overloads where it is not explicitly supplied, and often this should be sufficient. See more about uniqueness monitoring below.

First lets write some events to the error channel. This demonstrates all of the various built-in overloads such as logging an exception or logging a format string or both.

```C#
using ExpressiveLogging;

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
```

The LogManager.Logs property returns an object implementing IExpressiveLogs, which is an interface to simplify access to the default log streams. There's no reason you must use this interface, and instead you can access a specific stream directly.

```C#
using ExpressiveLogging;

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
```
 
Assuming you want to use IExpressiveLogs directly you may call the provided extension methods to write to the default streams as needed.

```C#
using ExpressiveLogging;

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
```

### Thread Safety
Unless otherwise noted / known in a specific configuration, an ILogStream should be treated thread-unsafe. Each thread is expected to use a unique ILogStream instance. The StaticLogRepository handles this detail for you when you are following along in these examples, so this only comes into play when dealing with more advanced scenarios. 

### Counter Logging
Writing to counters requires additional counter tokens to be created, one for each counter that is being written to. there
are two types of counters: raw (IRawCounterToken), and named (INamedCounterToken). 

```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class MyComponent {
		static readonly IRawCounterToken _totalRequests = LogManager.CreateRawCounterToken("TotalRequests");
		static readonly IRawCounterToken _activeRequests = LogManager.CreateRawCounterToken("ActiveRequests");
		static readonly INamedCounterToken _processTime = LogManager.CreateNamedCounterToken("ProcessTime");
	}
}
```

INamedCounterToken counters are special because they take an extra ILogToken argument which serves as a key. There is a separate counter value tracked for each unique ILogToken that is used.

IRawCounterToken counters on the other hand directly represents a single counter value, and any change to that counter will directly affect the value across the entire application.

Depending on how many counters you are creating it may become more convenient to use a named counter as opposed to initializing new raw counters every time you need one. Frequently you will already have logging tokens defined within your components that can be used to publish data to named counters without extra allocations or other planning being required. 

One guidline to consider, use an INamedCounterToken when you are logging conceptually identical data that is reported in the same way by multiple different components. On the other hand, if there is data that is globally *unique* to the application, use an IRawCounterToken.

Once the appropriate counter type is selected it is trivial to report counter data:

```C#
using ExpressiveLogging.V3;

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

			log.IncrementCounterBy(_processTime, _lt, timer.ElapsedMilliseconds);
			log.DecrementCounter(_activeRequests);
		}
		public void SubComponentCounterExample(ILogStream log) {
			Stopwatch timer = Stopwatch.StartNew();

			// do additional work here

			log.IncrementCounterBy(_processTime, _sclt, timer.ElapsedMilliseconds);
		}
	}
}
```

### Context and Scoping 
The concept of capturing context data and scoped logging functionality overlap and need to be covered in parallel. A scoped operation is simply one that has a conceptual beginning and ending point.

A simple scoped operation leveraging IDisposable just makes explicit the process of recording these events.

```C#
using ExpressiveLogging.V1;

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
```

The API for capturing context data is built ontop of this scoping concept and gives a clear
starting and ending point for context data being captured. 

Any data that can be formatted into a string can be captured as context data, and all captured
context data can then be accessed by the logging stack to enrich logging events.


```C#
using ExpressiveLogging.V1;

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
```

The code above creates a new scope and defines a Tracking field and a ThreadID field in relation to the scope.
This contextual data can now be accessed by the logging stack from anywhere within the application that is 
executing within this scope. Log formatters could render these fields automatically for every event emitted,
or it could specifically be used to affect behavior of the logging subsystem creating different behavior in 
a special context.

There exists a special class ScopedActionLogStream which can be included in your logging stack to 
define special actions to take during the creation and deletion of scopes. The most obvious
use cases for this would be to write an extra event to the stream at the beginning and ending of each scope lifecycle,
or to inject some custom context data automatically every time a scope is created.

```C#

using ExpressiveLogging.V1;

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
      );
    }
    public static void SetupContextModifyingScopedActionStream() {
      var auditProxy = StaticLogRepository.GetProxy(LogManager.STREAM_TOKEN_AUDIT);

      // NOTE: For best performance, only use a single scoped action for all scoped behaviors, and inject it somewhere appropriate in the middle of your stack instead of into the proxy
      auditProxy.Add(new ScopedActionLogStream(
        (lt, parameters)=>{
          parameters.Add(new KeyValuePair<string,object>("ScopeCreationTime", (object)DateTime.Now.ToString()));
        },
        (lt, parameters)=>{},
        new NullLogStream()
      );
    }
	}
}
```

By invoking the two static methods SetupMessagingScopedActionStream() and SetupContextModifyingScopedActionStream() during startup,
the two scoped action handlers will execute their delegates every time a new scope is created. The first log stream will emit events on the Audit channel recording the creation and termination of each scope. The second stream will attach a context data item ScopeCreationTime with the current timestamp.

# Defining Your Logging Stack
The logging components are minimal and composable. You need to create a stack of components to create a logging system that is production ready. This extreme modularity makes the process of defining your stack more difficult, but gives the greatest control and the best API.

The top of the logging stack is the component that evaluates each event first, the bottom of the stack would be the last component to evaluate an event.

Below is a list of components and rough descriptions of their purpose:
	
FilteringLogStream 
: This class can be used to stop events from being evaluated further. The most common thing you would do with a filter is disable events on the Debug and Info streams of the convention based logging built into the library. Filters near the top of the stack yield greater performance benefits because less work is performed on events that are eventually ignored / dropped.

UniquenessCodeGenerator
: This class takes any events that don't have a uniqueness code, and adds one automatically. This is something almost always useful to include for the sake of generating consistent uniqueness codes. This is a tranformative component that should be placed somewhere in the middle of your stack, after you are done filtering, to tweak the event before you actually render the event.

ExceptionLogStreamFormatter
: This class provides an API for configuring the custom formatting of an exception to a string. AggregateException, for example, can have multiple inner exceptions. SqlException has custom fields for error number and sql script line number that you will completely miss if you just call ToString(). This class lets you configure the custom formatting for each of these special exception types and you almost always will want to have one of these in your stack. This is a tranformative component that would normally be placed somewhere in the middle of your stack, after  filtering, to tweak the event before rendering.

DefaultTextLogStreamFormatter
: This class implements a basic formatter that combines the Exception, Uniqueness code, format message and format data into a string that can be written to a text file, the console or any other place a string would be accepted. It doesn't do anything fancy and you *probably* should implement your own version eventually, but this is an acceptable starting point for those who don't demand any specific features in their log format. This rendering component should usually be placed near the bottom of the stack, where it will only operate on events that have undergone filtering and other preprocessing.

ExceptionHandlingLogStream
: This class catches exceptions thrown by the logging stack and configures an action to take place when this error happens. This component is one you almost always will want to include in your stack, because if your logging code is broken, you don't want it breaking other parts of your system. This component should usually be near the top of the stack, depending on your risk tolerance and confidence in the logging code, you could potentially place this below filtering for better performance, or at the very top of the stack for maximum safety.

EmptyFormatMessageFixer
: When you pass a string to string.Format() but don't pass any format data, string.Format() still parses the string, and has certain syntax requirements for characters such as '{' and '}' which can lead to format exceptions getting thrown, even though the real intention was just to write a string that contained a special character. This class detects the case where no format data is provided with a format string, and disables the parsing mechanism, preventing that potential source of errors.

CompositeLogStream
: This class multiplexes each event to several underlying streams. This can become helpful as your stack becomes more complex, allowing a single event to be sent to multiple rendering components or otherwise undergo several different modes of processing simultaneously. Where it is placed in the stack would almost always depend on your use cases.

ProxyLogStream
: The proxy log stream enables dynamically changing the logging stack, at a specific place in the stack, in realtime, safely. It exposes API to add or remove an ILogStream to the stack at the place where the ProxyLogStream was placed. In the case that multiple streams have been added to the proxy, a CompositeLogStream will be used automatically. In the case no streams are assigned to the proxy, events will be sent to a NullLogStrema. This component is integral to the StaticLogRepository to define the separation between the Logging Stack and the final event stream output. It can be used in other scenarios where changing the stack dynamically, and in complex ways, may be desirable.

BufferedLogStream
: This class writes events to a thread safe queue. It does a lot of sophisticated work to capture thread-local context data and create closures around thread local storage so that the events can be transferred to a diferent thread (somewhat) safely. If you are trying to offload your logging to a background thread, or if you are trying to redirect all of your multi-threaded log rendering to a special UI thread, this is the workhorse that you use to do so. Generally a buffered log stream would be put at the bottom of the stack in place of another rendering component so that the actual rendering can be implemented by replaying the buffered streams.

AssertionLogStream
: This class performs buffering of the events (using BufferedLogStream), and then adds an API for testing assertions about them. You can verify that messages contain specific data or make general checks about how much data is being written. This is often paired with a FilteringLogStream to make sure that only events you care about are making it into the LogStream but can be used to test large swathes of your system in bulk as well. In a normal unit test scenario you might consider creating a CompositeLogStream (which can send one event to multiple places), sending all events to StdoutLogStream or TraceLogStream (so they appear in your unit testing logs verbatim), but also send events to a FilterLogStream -> AssertionLogStream that you will check in your unit tests for a specific component, subsystem or system.

TextWriterLogStream
: This class renders messages verbatim to and underlying TextWriter object. You can use it equally well to write to a network socket or a file and a variety of other destinations depending on your needs. It is a rendering component generally found at the bottom of the stack.

StdoutLogStream
: Messages send here get written to the console stdout. It is a rendering component generally found at the bottom of the stack.

StderrLogStream
: Messages sent here get written to the console stderr. It is a rendering component generally found at the bottom of the stack.

TraceLogStream
: Messages sent here get written to the System.Diagnostics.Trace logging system. It is a rendering component generally found at the bottom of the stack.

NullLogStream
: Messages sent here are dropped and nothing is executed and nothing goes anywhere. This isn't often useful but if you just want to ignore everything in the logging subsystem this would be how you do it. It is a rendering component generally found at the bottom of the stack.

ScopedActionLogStream
: This specialized log stream provides the ability to execute custom code each time a new scope is created, and terminated. The actions can vary based on your application though some common examples have been discussed in the Context and Logging Scope section of this document. This stream can be placed almost anywhere in the stack however for best performance it would likely need to be placed somewhere after filtering but before rendering.

DelegatingLogStream
: This specialized log stream is a base class intended for implementing custom stream formatters. The underlying implementation simply forwards all API methods to an inner ILogStream provider. Deriving from this class and overriding specific methods of the ILogStream interface provides some convenience over rolling your own forwarding mechanism for APIs that may not actually be relevant to your custom stream formatter.

PowershellV5Logging.RunspaceLogging.CmdletErrorLogStream
: Messages sent here are written to the powershell runspace using the WriteError API. This can become a somewhat more integrated experience than using stdout / stderr for powershell specific use cases. This is exposed through a separate ExpressiveLogging.PowershellV5 nuget package.

PowershellV5Logging.RunspaceLogging.CmdletInformationLogStream
: Messages sent here are written to the powershell runspace using the WriteInformation API. This can become a somewhat more integrated experience than using stdout / stderr for powershell specific use cases. This is exposed through a separate ExpressiveLogging.PowershellV5 nuget package.

PowershellV5Logging.RunspaceLogging.CmdletDebugLogStream
: Messages sent here are written to the powershell runspace using the WriteDebug API. This can become a somewhat more integrated experience than using stdout / stderr for powershell specific use cases. This is exposed through a separate ExpressiveLogging.PowershellV5 nuget package.

PowershellV5Logging.RunspaceLogging.CmdletVerboseLogStream
: Messages sent here are written to the powershell runspace using the WriteVerbose API. This can become a somewhat more integrated experience than using stdout / stderr for powershell specific use cases. This is exposed through a separate ExpressiveLogging.PowershellV5 nuget package.

PowershellV5Logging.RunspaceLogging.CmdletWarningLogStream
: Messages sent here are written to the powershell runspace using the WriteWarning API. This can become a somewhat more integrated experience than using stdout / stderr for powershell specific use cases. This is exposed through a separate ExpressiveLogging.PowershellV5 nuget package.


### Suggested Configuration
The first consideration is where you want your log events to be written. A sensible choice in general might be TraceLogStream. A TextWriter to disk, or a combination of StdOut and StdErr might be good choices depending on your needs.

Once you have decided on the destination for events, there are stream formatters and filters to put above those destinations to create a complete stack.
It would generally be a good idea to include the UniquenessCodeGenerator and ExceptionLogStreamFormatter. For any destination that actually performs
a textual output, you are undoubtedly going to want a formatting. The DefaultTextLogStreamFormatter will do this job until a more advanced solution is needed.


```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class ApplicationLogInitializer {
    public static void InitializeLogging() {
      StaticLogRepository.Init(ApplicationLogInitializer.LogFactoryBuilder);
      StaticLogRepository.SetTokenTransform(ApplicationLogInitializer.TransformTokens);
    }
    public static ILogStreamToken TransformTokens(ILogStreamToken tok) {
      switch (tok.Name) {
        case LogManager.STREAM_TOKEN_ALERT.Name: 
        case LogManager.STREAM_TOKEN_ERROR.Name:
        case LogManager.STREAM_TOKEN_WARNING.Name:
        case LogManager.STREAM_TOKEN_AUDIT.Name:
        case LogManager.STREAM_TOKEN_INFO.Name:
        case LogManager.STREAM_TOKEN_DEBUG.Name:
          return tok;
        default: return LogManager.LogManager.STREAM_TOKEN_WARNING;
      }

    }
    public static Func<ProxyLogStream, ILogStream> LogFactoryBuilder(ILogStreamToken tok) {
      switch (tok.Name) {
        case LogManager.STREAM_TOKEN_ALERT.Name: return ApplicationLogInitializer.BuildAlertStream;
        case LogManager.STREAM_TOKEN_ERROR.Name: return ApplicationLogInitializer.BuildErrorStream;
        case LogManager.STREAM_TOKEN_WARNING.Name: return ApplicationLogInitializer.BuildWarningStream;
        case LogManager.STREAM_TOKEN_AUDIT.Name: return ApplicationLogInitializer.BuildAuditStream;
        case LogManager.STREAM_TOKEN_INFO.Name: return ApplicationLogInitializer.BuildInfoStream;
        case LogManager.STREAM_TOKEN_DEBUG.Name: return ApplicationLogInitializer.BuildDebugStream;
        default: return ApplicationLogInitializer.BuildOtherStreams;
      }
    }
    public static ILogStream BuildOtherStreams(ProxyLogStream stream) {
      return BuildWarningStream(stream);
    }
    public static ILogStream BuildStandardStack(ProxyLogStream stream) {
      return new ExceptionHandlingLogStream(
        new UniquenessCodeGenerator(
          new EmptyFormatMessageFixer
            new ExceptionLogStreamFormatter(
              new DefaultTextLogStreamFormatter(
                stream
      ))));
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
```

This configuration shwon above supports the built-in logging streams defined in LogManager, and redirects events written to any  other user defined streams to the Warning stream through a token transform. The stack used for each log stream is basically identical, however the Alert / Error / Warning streams write to stderr while the audit stream writes to stdout. Info and debug streams are being configured with the NullLogStream meaning that any events emitted on those streams will be ignored.

The last component to consider is filtering at the top of the stack, the filter will short-circuit events that are not relevant to your current use case. The earlier you filter out events the less time spent doing work you don't need to do.


``` C#
namespace MyLibrary {
	public partial class ApplicationLogInitializer {
		public static ILogStream StreamFilter(ILogStream stream) {
			return FilterManager.CreateStream(stream,
				FilterManager.CreateFilter(
					ApplicationLogInitializer.MessageFilter,
					ApplicationLogInitializer.RawCounterFilter,
					ApplicationLogInitializer.NamedCounterFilter
				));
			}
			public static bool MessageFilter(ILogToken token) {
				if (token.Name == "DONT WRITE THIS TOKEN") {
					return false;
				}

				return true;
			}
			public static bool RawCounterFilter(IRawCounterToken counter) {
				if (counter.Name == "DONT USE THIS COUNTER") {
					return false;
				}

				return true;
			}
			public static bool NamedCounterFilter(INamedCounterToken counter, ILogToken token) {
				if (counter.Name == "DONT USE THIS COUNTER") {
					return false;
				}
				if (token.Name == "DONT USE THIS TOKEN") {
					return false;
				}

				return true;
			}
		}
	}
}
```

As demonstrated, returning false from these filter methods will inhibit those events from propogating further through the stack of event handlers while returning true will pass the event onwards.

# Tests
There are some basic automated tests included with this project. These tests do not currently cover advanced
functionality but achieve rudimentary code coverage with minimal effort. A more complete test suite
would be highly desirable.

# Build Notes
 * ```dotnet build``` to compile 
 * ```dotnet pack -c release``` to generate nuget package

# Licensing
This code is released under an MIT license. 

# Changelog

[For Release Notes See Here](ExpressiveLogging.ReleaseNotes.md)
