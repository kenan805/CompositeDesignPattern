using System;
using System.Collections.Generic;

namespace CompositeDesignPattern
{
    interface IGraphic
    {
        public void Move(double x, double y);
        public void Draw();

    } //Component

    class Dot : IGraphic
    {
        protected double _x;
        protected double _y;

        public Dot(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public void Draw()
        {
            Console.WriteLine($"Dot draw x: {_x} y: {_y}");
        }

        public void Move(double x, double y)
        {
            Console.WriteLine($"Dot move to x: {_x += x} y: {_y += y}");
        }
    } //Leaf

    class CompoundGraphic : IGraphic
    {
        public List<IGraphic> Children { get; set; } = new List<IGraphic>();

        public void Add(IGraphic graphic)
        {
            Children.Add(graphic);
        }

        public void Remove(IGraphic graphic)
        {
            Children.Remove(graphic);
        }

        public void Draw()
        {
            foreach (var child in Children)
            {
                child.Draw();
            }
        }

        public void Move(double x, double y)
        {
            foreach (var child in Children)
            {
                child.Move(x, y);
            }
        }
    } //Composite

    class Circle : Dot
    {
        public double Radius { get; set; }
        public Circle(double x, double y, double radius) : base(x, y)
        {
            Radius = radius;
        }
        public new void Draw()
        {
            Console.WriteLine($"Circle draw x: {_x} y: {_y} radius: {Radius}");
        }
    }

    class ImageEditor
    {
        public CompoundGraphic All { get; set; }

        public void Load()
        {
            All = new CompoundGraphic();
            All.Add(new Dot(1, 2));
            All.Add(new Circle(5, 3, 10));
        }

        public void GroupSelected(IGraphic[] components)
        {
            var group = new CompoundGraphic();
            foreach (var component in components)
            {
                group.Add(component);
                All.Remove(component);
            }
            All.Add(group);
            All.Draw();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            ImageEditor imageEditor = new ImageEditor();
            IGraphic[] graphics = new IGraphic[]
            {
                new Dot(2,4),
                new Circle(3,6,2)
            };
            
            imageEditor.Load();
            imageEditor.GroupSelected(graphics);
        }
    }
}
