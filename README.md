# Versioning
This is version 2.0.0 of the expressive logging library

The base package is available from nuget at: https://www.nuget.org/packages/ExpressiveLogging/2.0.0

The powershell V5 package is available from nuget at: https://www.nuget.org/packages/ExpressiveLogging.PowershellV5Logging/2.0.0

The source for this release is available on github at: https://github.com/hannasm/ExpressiveLoggingDotNet/releases/tag/2.0.0

# About
The expressive logging project attempts to provide a robust logging framework that leverages lambda expressions to provide
deeply integrated, high performance logging with a deep feature set. The primary advantage offered through the use of 
lambda expressions comes from being able to disable and enable logging components at runtime, and achieving a maximal
reduction in overhead when those components are disabled.

Logging is driven by two major components, an ILogStream and ILogToken.

ILogStream provides an API for recording application events. This includes three key pieces
  * Capturing long lived context data, this data is associated with each event as it occurs
  * Capturing performance and system metrics through counters
  * Capturing system behavior through the event system

ILogToken is the identifier which is used to distinguish the source of logging events, and used to uniquely identify Counters
 
ILogStream implementations exist for a variety of different use cases and new ones may created cheaply and easily
to integrate expressive log streams into components that need them, or with any client applications that may
have different logging needs.

ILogStream is composable, primarily through the use of a CompositeLogStream. Using composability, it is possible to direct a single application event to multiple destinations.

There are a number of extensions and utilites that enhance the core behavior.
  * The ExpressiveLogging.Filters namespace provides a solution for controlling which events are broadcast
  * The ExpressiveLogging.StreamFormatters namespace includes a number of classes to control the output format, and simplify the process of creating your own formatters

# Event streams
Event streams are most easily thought of as simple logging / tracing messages that are embedded within an application or
component. Each event has an identifier composed of it's log token, uniqueness code, and the message payload (which 
can include some or all of an exception, a format string, and an array of elements to inject into the format string.

There are multiple channels, each intended for different priorities and types of events: 

	* Alert - alerts are the highest importance events, that would warrant immediate attention. These are the ones you are most likely to send via email / sms / etc..
	* Error - this is a slightly lower priority event, that indicates a real issue, but one that might not demand immedaite attention
	* Warning - this is intended for lower priority issues, perhaps for situations that are expected edge cases, and things that may or may not have a clear solution
	* Audit - these are considered non-error related events that are of high priority. Audit events are the logging equivalent of writing to standard output.
	* Info - these are a lower-priority version of audit events, which would only be captured when specifically requested
	* Debug - these are even lower-priority events, which should only be captured during development / debugging scenarios

Event stream payloads consist of a token, an exception, a uniqueness code, and a format string. 

* The logging token is the highest level classifier of an event, and primarily is used to indicate the component and subsystem from which the event took place.
* The uniqueness code is intended to allow tooling to identify events without parsing. This is supposed to be a hash over the exception / format string / any relevant data that would uniquely identify the event.
* The exception and format string are the high detail payload that is formatted to the underlying output of each log stream.

# Performance Counters
The performance counter subsystem is a means of visualizing system health and behavior at a higher level of detail. Each performance counter stores an integer, representing a state which will change over time. Making observations of this state gives an immediate realtime view of some particular behavior in the system. Making repeated observations of this state, and using various statistical processes to analyze those values, can lead to surprisingly useful views of a system with minimal overhead.

Recording data to a counter is drastically more efficient for the application, and the consumer of the counter data, when running at high speed and with high volume.

For example, a component handling 1000 requests per second wants to record how long it takes to execute. If that component writes the time of each message to an event stream, that means 1000 messages per second are being recorded to disk (or some other medium) and then an administrator needs to sift through each of those 1000 messages per second to asses how the system behaved. In contrast, if this same component adds that time to a counter, and the administrator samples that counter each second. Then the difference between the previous observation and the current is the total amount of time spent processing. If this componenbt also tracks how many requests were processed it is possible to calculate average runtime. 

There are no default implementations to this subsystem at the present time, but it is entirely possible to implement a custom log stream for any of a number of third party performance counter subsystems.

# Context Tracking
This logging system supports tracking context data. The most obvious forms of context might include process ID, thread ID, and various other correlation ids relevant to the system. Context data is stored in thread local storage, and in such a way that the standard .NET threading model will retain that data during continuations through the thread pool.

Context data can transition across thread boundaries and should only store primitive data types like strings.

ILogStream is deeply integrated into the context management functionality and implementors can inject custom data into the context, as well as create event handling behaviors for both the beginning and ending of context bonudaries.

The context data api allows nested contexts, and overlapping nested context data defines a hierarchy, with the leaf nodes taking precedence 

All of this probably way more complicated than it really needs to be but if you don't use it it won't cost you anything and if
you do happen to want it, well you are in luck.


# Usage
The first step in implementing any logging functionality is defining the logging token(s)

```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class MyComponent {
		private static readonly ILogToken _lt = LogManager.GetToken(); // # Name = MyLibrary.MyComponent
		private static readonly ILogToken _sclt = LogManager.GetToken(_lt, "SubComponent"); // # Name = MyLibrary.MyComponent.SubComponent
		private static readonly ILogToken _dynlt = LogManager.GetToken("DynamicComponent"); // # Name = DynamicComponent
	}
}
```

Logging tokens can be initialized automatically. The parameterless overload of GetToken() used to initialize _lt uses the stack to derive a token name automatically. In this case 'MyLibrary.MyComponent'.  Logging tokens can be initialized as a derivation of an existing logging token. The overload used to initialize _sclt is based on the _lt
token, with a period '.' and the word SubComponent added to the end. Logging tokens can be initialized using arbitrary strings as well. The overload used to oinitialize _dynlt creates a logging token with a completely user-defined name.

Logging tokens initialized as static fields have a minimal performance / memory cost but in general are very cheap to create.

### Logging Events
Events written to the event stream have an exception, uniqueness code, and format string. Events can be written to one channel. Only 
an exception or a message is strictly required. The uniqueness code will be generated automatically if one is not explicitly set.

First is an example of each of the various overloads for the Error() channel.

```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class MyComponent {
		public void ErrorChannelLoggingExamples(ILogStream log) {
			log.Error(_lt, m=>m(new Exception("An error ocurred"))); // exception only
			log.Error(_lt, m=>m(new Exception("An error ocurred"), "Format String")); // exception and raw string
			log.Error(_lt, m=>m(new Exception("An error ocurred"), "Format String {0} - {1}", DateTime.Now, 20)); // exception and format string
			
			log.Error(_lt, m=>m(new Exception("An error ocurred", -1))); // exception plus uniqueness code
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

Each channel has identical overloads, and can be used interchangably from an API perspective.

```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class MyComponent {
		public void EachChannelLoggingExample(ILogStream log) {
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
Unless otherwise noted / known, an ILogStream should be considered thread-unsafe for the purposes of all development. Each thread
should be using a unique ILogStream instance.

The reccomended way to deal with this is to use parameter based dependency injection, meaning you pass ILogStream into each function
that needs it.

```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class MyComponent {
		public void ParameterInjectedLogging(ILogStream log) {
			try {
				// .. do stuff...
			}catch(Exception eError) {
				log.Error(_lt, m=>m(eError, "Eating exception while processing myMethod"));
			}
		}
	}
}
```

Sometimes this approach is going to be infeasible, and a ```StaticLogRepository``` class is provided that can be used to access an appropriate LogStream without it being provided as a parameter.

```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class MyComponent {
		public void StaticLogging() {
			var log = StaticLogRepository.GetLogger();
			try {
				// .. do stuff...
			}catch(Exception eError) {
				log.Error(_lt, m=>m(eError, "Eating exception while processing myMethod"));
			}
		}
	}
}

// At some point before MyComponent runs you will need to initialize the StaticLogRepository
StaticLogRepository.Init(()=>new ConsolLogStream());
```

### Counter Logging
Counter logging requires additional counter tokens to be defined, one for each counter that is being written to. there
are two types of counters: raw (IRawCounterToken), and named (INamedCounterToken). 

```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class MyComponent {
		private static readonly IRawCounterToken _totalRequests = LogManager.CreateRawCounterToken("TotalRequests");
		private static readonly INamedCounterToken _processTime = LogManager.CreateRawCounterToken("ProcessTime");
	}
}
```

Named counters are special because they are uniquely idenetified using both a counter token (INamedCounterToken), and 
a logging token (ILogToken).

Raw counters are the lowest level representation of a counter, with a single distinct name that is defined
in the associated IRawCounterToken object.


Choosing between the two can often become an implementation defined task, and depending on what underlying system
is handling your counters, the exact behavior may differ. Other implementation defined considersations might include 
how the counters are expected to interact across process boundaries and units of measure or other data 
formatting / rendering differences that may need to evolve over time.

In general, it makes sense to use an INamedCounterToken when you are logging conceptually identical data, that is differentiated 
by which component that data is coming from. On the other hand, any data that is a globally *unique* concept to the application
tested would be more appropriate for a IRawCounterToken.

Once the appropriate counter type is selected it is fairly trivial to implement the various subtasks involved in tracking counter data:

```C#
using ExpressiveLogging.V1;
using System.Diagnostics;

namespace MyLibrary {
	public partial class MyComponent {
		public void CounterExample(ILogStream log) {
			log.IncrementCounterBy(_totalRequests, 1);
			Stopwatch timer = Stopwatch.StartNew();

			// do additional work here

			SubComponentCounterExample(log);

			// do additional work here

			log.IncrementCounterBy(_processTime, _lt, timer.ElapsedMilliseconds)
		}
		public void SubComponentCounterExample(ILogStream log) {
			Stopwatch timer = Stopwatch.StartNew();

			// do additional work here

			log.IncrementCounterBy(_processTime, _sclt, timer.ElapsedMilliseconds)
		}
	}
}
```

### Context and Scoping 
The concept of capturing context data and a separate but equally useful feature, scoped logging 
overlap and need to be covered in parallel. A scoped operation is simply one that has a defined beginning and ending point.

A simple scoped operation leveraging IDisposable just makes explicit the process of recording these events.

```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class MyComponent {
		public void SimpleScopedExample(ILogStream log) {
			using (LogManager.NewScope(log, _lt))
			{
				// logger generates event 'Begin Scope'

				// some behavior takes place here

				// logger generates event 'End Scope'
			}

		}
	}
}
```
A scope can be customized with special logging if the default messaging is not adequate:

```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class MyComponent {
		public void CustomScopeMessageExample(ILogStream log) {
			using (LogManager.NewScope(
				log, _lt, 
				m=>m("Begin CustomScopeMessageExample"), 
				m=>m("End CustomScopeMessageExample")
			))
			{
				// logger generates event 'Begin CustomScopeMessageExample'

				// some behavior takes place here

				// logger generates event 'End CustomScopeMessageExample'
			}

		}
	}
}
```

This task of recording scope is a fundamental feature that takes simple event / logging and defines 
two correlated points of interest as the begining and ending, instead of typical logging
as a single point in time.

The API for capturing context data is built ontop of this scoping concept and gives a clear
starting and ending point for context data to be captured, and also defines a lifetime for that context data
to live before being released.

Any data that can be formatted into a string can be captured as context data, and all captured
context data can then be leveraged by the underlying logging implementations to provide useful
information that is not directly included in the logging events.


```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class MyComponent {
		public void ContextCaptureExample(ILogStream log) {
			using (LogManager.BuildScope(_dynlt).
				AddContext("Tracking", Guid.NewGuid().ToString("N")).
				AddContext("ThreadID", System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()).
				NewScope(log)				
			))
			{
				// logger generates event 'Begin Scope'

				// some behavior takes place here
				
				// logger generates event 'End Scope'
			}
		}
	}
}
```

# Defining Your Logging Stack
The logging components are implemented as simple and minimal components, and in a composable way. You need more than one Logging component to do anything useful. This extreme modularity makes the process of defining your stack much more difficult, but it also gives you the most control.

Below is a list of the components and rough descriptions of their purpose:
	
FilteringLogStream 
: This class can be used to stop certain messages from being recorded. The most common thing you would do with a filter is disable Debug and Info events.

UniquenessCodeGenerator
: This class takes any events that don't have a uniqueness code, and adds one automatically. This is something you almost always want, but it's a component you must enable, not a feature that is built-in.

ExceptionLogStreamFormatter
: This class provides an API for configuring custom formatting of exception messages. AggregateException can have multiple inner exceptions. SqlException has custom fields for error number and sql script line number. This class lets you configure formatting for these types and you almost always will want to have one of these in your stack.

DefaultTextLogStreamFormatter
: This class implements a basic formatter that combines an Exception and a Uniqueness code, and a FormatString into a textual message that can be written to a text file or the console. It doesn't do anything fancy and different people want different formats, but this is a good starting point if you just want to write something and dont care how pretty it looks.

ExceptionHandlingLogStream
: This class catches exceptions thrown by the logging code and configures an action to take when this error happens. This is another component you almost always want to include in your stack, because if your logging code is broken, you don't want it breaking other parts of your system.

EmptyFormatMessageFixer
: When you pass a string to string.Format() but don't pass any format args, string.Format() still parses the string, and has certain requirements for characters such as '{' and '}' which can lead to an exception getting thrown, even though the intention was just to write a string that contained a special character. This class detects the case when no format args have been specified, and disables the parsing mechanism, hence preventing those errors.

CompositeLogStream
: This class multiplexes each logging event to several log streams. This can become really helpful as your stack becomes more advanced and you want messages going to different sources or handled in different ways.

BufferedLogStream
: This class writes all log events to a thread safe queue. It also does a lot of sophisticated work to capture thread-local context data and create closures around the thread local storage so that it can be read and written by a diferent thread. If you have a log that needs to be rendered on a separate UI thread (or any async logging) then this is the class for you.

AssertionLogStream
: This class takes buffered log events, and adds an API for making assertions on them. You can verify individual messages contain specific data or make more general assertions about the nature of logging taking place on different channels. This is best paired with a FilteringLogStream to make sure the logging events are predictable and restricted to whichever components(s) you are interested in testing.

TextWriterLogStream
: This class renders messages verbatim to and underlying TextWriter object. You can use it equally well to write to a network socket or a file and a variety of other destinations.

StdoutLogStream
: Messages send here get written to the consoles stdout

StderrLogStream
: Messages sent here get written to the consoles stderr

DebugLogStream
: Messages sent here get written to the System.Diagnostics.Debug logging system. This is a favorite of mine, that when present, allows realtime viewing of log events using a utility like DbgView.exe

TraceLogStream
: Messages sent here get written to the System.Diagnostics.Trace logging system. This is a favorite of mine, that when present, allows realtime viewing of log events using a utility like DbgView.exe

NullLogStream
: Messages sent here are dropped and nothing is executed and nothing goes anywhere. This isn't often useful but if you just want to disable everything in the logging subsystem this would be how you do it.

PowershellV5.RunspaceLogging.CmdletLogStream
: Messages sent here are written to the powershell runspace using the appropriate powershell APIs. This becomes a much more integrated experience than simply using stdout / stderr for powershell specific use cases. This is exposed through a separate nuget package.


### Suggested Configuration
The first consideration is where you want your log events to be written. A sensible choice in general is to use the TraceLogStream. A TextWriter or a combination of StdOut and StdErr might also be good choices depending on your needs.

An example for Directing error messages to StdErr and everything else to StdOut might look like

```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class MyComponent {
		public ILogStream CreateConsoleLogger() {
			return CompositeLogStream.Create(
				// Filter all errors and alerts to stderr
				FilterManager.CreateStream(
					new StderrLogStream(),
					FilterManager.CreateFilter(null, null, null, null, ()=>true, ()=>true, null, null)
				),
				// everything else to stdout
				FilterManager.CreateStream(
					new StdoutLogStream(),
					FilterManager.CreateFilter(()=>true, ()=>true, ()=>true, ()=>true, null, null, ()=>true, ()=>true)
				)
			);
		}
	}
}
```

Once you have decided on the final destination for events there are several relevant things to put in front of those destinations to improve usability.
It would generally be a good idea to include the UniquenessCodeGenerator and ExceptionLogStreamFormatter. For any destination that actually performs
a textual output, you also are undoubtedly going to want some sort of formatting. The DefaultTextLogStreamFormatter will do this job or a more advanced
solution.

The last component to consider is a filter as the very first step in the logging process, to completely bypass any logging events that are not interesting. The
earlier you filter out events the less time spent doing work you don't need to do.


```C#
using ExpressiveLogging.V1;

namespace MyLibrary {
	public partial class MyComponent {
		public ILogStream GetLogStack() {
			var exceptionFormatter = new ExceptionLogStreamFormatter(
				new DefaultTextLogStreamFormatter(
					CreateConsoleLogger()
				)
			);

			// Filter out debug and info messages, allow all others
			return FilterManager.CreateStream(
				new ExceptionHandlingLogStream(
					new UniquenessCodeGenerator(
						exceptionFormatter
					)
				),
				FilterManager.CreateFilter(()=>false, ()=>false, null, null, null, null, null, null)
			);
		}
	}
}
```

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
