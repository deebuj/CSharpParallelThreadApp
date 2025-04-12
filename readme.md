# C# Parallel Threading Demo Application

This application demonstrates various parallel processing patterns in C# using modern .NET features.

## Features

- Parallel task processing with controlled concurrency
- Implementation of Channel for producer-consumer pattern
- Usage of ConcurrentBag for thread-safe collection operations
- Demonstration of async/await patterns
- Configurable number of tasks and concurrent operations

## Implementation Details

The application showcases two different approaches to parallel processing:

### 1. Channel-based Implementation
- Uses `Channel.CreateBounded<T>` for controlled concurrent operations
- Demonstrates producer-consumer pattern
- Includes task processing with delays to simulate work
- Results are processed in order of completion

### 2. ConcurrentBag Implementation
- Uses thread-safe collection for parallel operations
- Demonstrates simple concurrent data collection
- Processes tasks with configurable delays

## Configuration

The application uses the following default settings:
- Total number of tasks: 10
- Maximum concurrent tasks: 2

## Requirements

- .NET 8.0
- Visual Studio 2022 or later recommended

## Running the Application

1. Clone the repository
2. Open the solution in Visual Studio
3. Build and run the application

## Output

The application will display:
- Task start notifications
- Processing progress
- Results from both Channel and ConcurrentBag implementations
- Completion status

## Project Structure

- `Worker.cs`: Contains the main parallel processing logic
- `Program.cs`: Application entry point and configuration
- Configuration files for different environments (Development, QA)