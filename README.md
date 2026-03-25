# 📞 AI Shipment Support Agent (Voice Call)

An AI-powered voice agent that allows users to **call a phone number and get real-time updates about their shipment**.

This project demonstrates how to build a **live phone-call AI agent using a pure .NET stack**, integrating telephony, LLMs, and agentic workflows.

---

## 🚀 Overview

Users can call the system via phone, and the AI agent will:

- Understand the user's query (via speech-to-text)
- Retrieve shipment-related information
- Respond intelligently using natural language (text-to-speech)
- Handle multi-turn conversations like a real support agent

---

## 🧠 Use Case

> "Where is my container?"  
> "What is the estimated time of arrival?"  
> "Has my shipment been delayed?"

The AI agent behaves like a **customer support executive**, but fully automated.

---

## 🏗️ Tech Stack

- **.NET 10** – Backend application framework  
- **Microsoft Agent Framework** – Agent orchestration and reasoning  
- **Azure OpenAI** – Natural language understanding and response generation  
- **Twilio** – Voice call handling (incoming/outgoing calls, speech input/output)

---

## 🔁 Call Flow

1. User calls the Twilio number  
2. Twilio forwards the request to the .NET API  
3. Speech input is converted to text  
4. Agent processes the query using:
   - Context
   - Shipment data
   - LLM reasoning  
5. AI generates a response  
6. Response is converted to speech  
7. User hears the answer in real-time  

---

## 🧩 Key Features

- 🎙️ Real-time voice interaction  
- 🤖 AI-powered conversational agent  
- 🔄 Multi-turn dialogue support  
- 📦 Shipment tracking integration  
- 🧠 Context-aware responses  
- ☁️ Cloud-native and scalable  
