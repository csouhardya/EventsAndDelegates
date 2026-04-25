# Events and Delegates in C# — Full Debugger Trace (Project Specific)

This project demonstrates how **events and delegates execute at runtime** using a publisher–subscriber pattern.

This document explains:

* Exact **line-by-line execution flow**
* How control jumps between files
* What happens internally during `+=` and event invocation
* A **Visual Studio-style debugger trace (F10 / F11 thinking)**

---

# 📁 Project Structure

```
Program.cs        → Entry point (wiring)
VideoEncoder.cs   → Publisher (raises event)
MailService.cs    → Subscriber 1
MessageService.cs → Subscriber 2
```

---

# 📄 VideoEncoder.cs (Publisher)

```csharp
1  public class VideoEncoder
2  {
3      public delegate void VideoEncodedEventHandler(object source, EventArgs args);
4  
5      public event VideoEncodedEventHandler VideoEncoded;
6  
7      public void Encode(Video video)
8      {
9          Console.WriteLine("Encoding video...");
10 
11         OnVideoEncoded();
12     }
13 
14     protected virtual void OnVideoEncoded()
15     {
16         if (VideoEncoded != null)
17         {
18             VideoEncoded(this, EventArgs.Empty);
19         }
20     }
21 }
```

---

## 🔍 Explanation

### Line 5

Event declaration

* Internally a delegate reference
* Initially: `null`

### Line 11

Calls method that raises the event

### Line 18 (Critical)

```csharp
VideoEncoded(this, EventArgs.Empty);
```

* Executes all subscribed methods
* Control jumps OUT to subscribers

---

# 📄 MailService.cs

```csharp
1  public class MailService
2  {
3      public void OnVideoEncoded(object source, EventArgs e)
4      {
5          Console.WriteLine("Sending mail...");
6      }
7  }
```

---

# 📄 MessageService.cs

```csharp
1  public class MessageService
2  {
3      public void OnVideoEncoded(object source, EventArgs e)
4      {
5          Console.WriteLine("Sending message...");
6      }
7  }
```

---

# 📄 Program.cs

```csharp
1  class Program
2  {
3      static void Main(string[] args)
4      {
5          var video = new Video();
6          var videoEncoder = new VideoEncoder();
7          var mailService = new MailService();
8          var messageService = new MessageService();
9  
10         videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
11         videoEncoder.VideoEncoded += messageService.OnVideoEncoded;
12 
13         videoEncoder.Encode(video);
14     }
15 }
```

---

## 🔍 Key Lines

### Line 10–11 (Subscription)

```csharp
videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
videoEncoder.VideoEncoded += messageService.OnVideoEncoded;
```

What happens internally:

```
VideoEncoded → [MailService.OnVideoEncoded, MessageService.OnVideoEncoded]
```

* No execution yet
* Only registration

---

### Line 13 (Trigger)

```csharp
videoEncoder.Encode(video);
```

* Control moves to `VideoEncoder.cs`

---

# 🧪 DEBUGGER TRACE (Step-by-Step Execution)

---

## ▶️ STEP 1 — Start

**Program.cs:3**

```
Main()
```

---

## ▶️ STEP 2 — Object Creation

**Program.cs:6**

```
new VideoEncoder()
```

State:

```
VideoEncoded = null
```

---

## ▶️ STEP 3 — Subscription

**Program.cs:10–11**

```
+=
```

State:

```
VideoEncoded → [MailService, MessageService]
```

---

## ▶️ STEP 4 — Trigger

**Program.cs:13**

```
Encode(video)
```

➡ Jump to `VideoEncoder.cs:7`

---

## ▶️ STEP 5 — Execution Starts

**VideoEncoder.cs:9**

```
Encoding video...
```

---

## ▶️ STEP 6 — Raise Event

**VideoEncoder.cs:11**

```
OnVideoEncoded()
```

➡ Jump to line 14

---

## ▶️ STEP 7 — Null Check

**VideoEncoder.cs:16**

```
VideoEncoded != null → TRUE
```

---

## ▶️ STEP 8 — Invoke (Critical)

**VideoEncoder.cs:18**

```
VideoEncoded(...)
```

Delegate contains:

```
[MailService, MessageService]
```

---

## ▶️ STEP 9 — First Subscriber

➡ Jump to `MailService.cs:3`

```
Sending mail...
```

---

## ▶️ STEP 10 — Second Subscriber

➡ Jump to `MessageService.cs:3`

```
Sending message...
```

---

## ▶️ STEP 11 — Return Flow

```
MessageService → VideoEncoder → Program → End
```

---

# 🧠 Full Call Stack

```
Main()                      [Program.cs]
 └── Encode()               [VideoEncoder.cs]
      └── OnVideoEncoded()
           └── Invoke()
                ├── MailService.OnVideoEncoded()
                └── MessageService.OnVideoEncoded()
```

---

# ⚠️ Important Observations

### This does NOT execute anything:

```csharp
+=
```

### This executes everything:

```csharp
VideoEncoded(...)
```

---

# 🎯 Final Execution Flow

```
Program → Subscribe → Encode → Raise Event → Invoke → Subscribers Execute
```

---

# 🧵 One-Line Summary

```
Register → Trigger → Broadcast → Execute
```

---

# ✅ What This Project Demonstrates

* Delegate as method contract
* Event as safe wrapper
* Multicast delegate execution
* Publisher–Subscriber pattern
* Loose coupling between components
