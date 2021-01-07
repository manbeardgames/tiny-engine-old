# TinyEngine
TinyEngine is a small lightweight engine for creating games using the MonoGame framework. 

**TinyEngine is still under development and is not recommended currently for production use.**

Since the focus of TinyEngine is simplicity, you will not find typical conveniences such as an Entity-Component-System (ECS) setup or other such methodologies. Instead, TinyEngine provides the bare minimum setup that is typically needed to start making a game without having to create it yourself.  

TinyEngine includes:
* `Engine`: This is the base engine that your `Game` class will inherit from. It provides access to all other features in TinyEngine.
* `Graphics`: The `Graphics` instance provides a managed solution for graphic management and presentation, including **independent resolution scaling**.
* `Input`: The input system provides common checks for keyboard, mouse, and gamepad input presses and releases, and a **virtual input system** for a more module approach to input management.
* `Scene`: TinyEngine has a built in scene system that includes a simple setup for transition effects when changing from one scene to the next.
* `SpriteBatchExtensions`: TinyEngine includes extension methods for the `SpriteBatch` to allow rendering of 2D primitives such at **points**, **lines**, **rectangles**, and **circles**.

As TinyEngine is still in development, there is currently no documentation to provide. All source code, however, is heavily commented including public `<summary>` comments.

## License
MIT License

Copyright (c) 2018 Christopher Whitley

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

