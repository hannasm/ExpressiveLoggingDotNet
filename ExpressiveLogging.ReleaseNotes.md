    * 3.0.0 - major rewrite of main interfaces and API surface that drastically reduces size of codebase and improves utility
    * 3.0.0 - introduce more complete 'global' logging mechanism which generally makes adoption easier for everyone
    * 3.0.0 - delete DebugLogStream, replace any existing uses with TraceLogStream, they were functionally equivalent
    * 3.0.0 - add new stream classes ProxyLogStream and ScopedActionLogStream 
    * 3.0.0 - moved most classes to a single namespace and all this code is now under ExpressiveLogging.V3
    * 2.0.1 - rename namespace bufferlogging to bufferedlogging
    * 2.0.0 - update to project to target .NET Standard 2.0
    * 1.1.0 - addition of some extension methods (in root ExpressiveLogging.~version~ namespace) to assist in performance counter development
    * 1.0.3 - Fix: Buffering logger was popping an extra record on every ExecuteBuffer that didn't empty the queue completely
    * 1.0.2 - AssertableLogStream had some bugs that made it unusable, fixed those and tested this code thoroughly
    * 1.0.1 - fix for issue with assembly refactoring script, that caused corrupted dll (assembly bind failures)
    * 1.0.0 - This is the first release and includes all sorts of new code
    * 1.0.0 - there has been minimal testing
    * 1.0.0 - There are all sorts of things that should still be added to this library that aren't there yet
