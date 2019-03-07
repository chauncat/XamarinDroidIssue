using System;
using System.Runtime.CompilerServices;

public interface ILogger
{
    void Trace(string message, [CallerMemberName] string memberName = "", [CallerFilePath]string sourceFile = "", [CallerLineNumber] int sourceLineNumber = 0);
    void Trace(string message, Exception exception, [CallerMemberName] string memberName = "", [CallerFilePath]string sourceFile = "", [CallerLineNumber] int sourceLineNumber = 0);
    void Debug(string message, [CallerMemberName] string memberName = "", [CallerFilePath]string sourceFile = "", [CallerLineNumber] int sourceLineNumber = 0);
    void Debug(string message, Exception exception, [CallerMemberName] string memberName = "", [CallerFilePath]string sourceFile = "", [CallerLineNumber] int sourceLineNumber = 0);
    void Info(string message, [CallerMemberName] string memberName = "", [CallerFilePath]string sourceFile = "", [CallerLineNumber] int sourceLineNumber = 0);
    void Info(string message, Exception exception, [CallerMemberName] string memberName = "", [CallerFilePath]string sourceFile = "", [CallerLineNumber] int sourceLineNumber = 0);
    void Warn(string message, [CallerMemberName] string memberName = "", [CallerFilePath]string sourceFile = "", [CallerLineNumber] int sourceLineNumber = 0);
    void Warn(string message, Exception exception, [CallerMemberName] string memberName = "", [CallerFilePath]string sourceFile = "", [CallerLineNumber] int sourceLineNumber = 0);
    void Error(string message, [CallerMemberName] string memberName = "", [CallerFilePath]string sourceFile = "", [CallerLineNumber] int sourceLineNumber = 0);
    void Error(string message, Exception exception, [CallerMemberName] string memberName = "", [CallerFilePath]string sourceFile = "", [CallerLineNumber] int sourceLineNumber = 0);
    void Fatal(string message, [CallerMemberName] string memberName = "", [CallerFilePath]string sourceFile = "", [CallerLineNumber] int sourceLineNumber = 0);
    void Fatal(string message, Exception exception, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFile = "", [CallerLineNumber] int sourceLineNumber = 0);
}