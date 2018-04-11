using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSTestApp.Fundamentals;
using NUnit.Framework;

namespace MSTestApp
{
    [TestFixture]
    class StackTests
    {

        private Stack<string> _stack { get; set; }

        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<string>();
        }

        [Test]
        public void Push_WhenObjectIsNull_ThrowArgumentNullException()
        {

            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);

        }

        [Test]
        public void Push_WhenObjectIsNotNull_ReturnObject()
        {

            _stack.Push("a");

            Assert.That(_stack.Count, Is.EqualTo(1));

        }
        
        [Test]
        public void Count_EmptyStack_ReturnZero()
        {
            
            Assert.That(_stack.Count, Is.EqualTo(0));

        }

        [Test]
        public void Pop_EmptyStack_ThrowInvalidOperationException()
        {

            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);

        }

        [Test]
        public void Pop_StackWithAFewObjects_ReturnObjectOnTheTop()
        {

            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            var result = _stack.Pop();

            Assert.That(result, Is.EqualTo("c"));

        }

        public void Pop_StackWithAFewObjects_RemoveObjectOnTheTop()
        {

            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            _stack.Pop();

            Assert.That(_stack.Count, Is.EqualTo(2));

        }

        [Test]
        public void Peek_EmptyStack_ThrowInvalidOperationException()
        {

            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);

        }

        [Test]
        public void Peek_StackWithObjects_ReturnObjectOnTop()
        {

            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            var result = _stack.Peek();

            Assert.That(result, Is.EqualTo("c"));

        }

        [Test]
        public void Peek_StackWithObjects_DoesNotRemoveTheObjectOnTop()
        {

            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");

            _stack.Peek();

            Assert.That(_stack.Count, Is.EqualTo(3));

        }
    }
}
