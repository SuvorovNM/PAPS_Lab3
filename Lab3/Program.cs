using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    enum InterfaceType { Window, Panel, Button, InputField, Picture, HyperLink}
    //component
    abstract class InterfaceElement
    {
        protected string _name;
        protected InterfaceType _type;

        public InterfaceElement(string name, InterfaceType type)
        {
            this._name = name;
            this._type = type;
        }

        public abstract void Add(InterfaceElement el);
        public abstract void Remove(InterfaceElement el);
        public abstract void Display(int indent);
    }

    class PrimitiveElement : InterfaceElement
    {
        public PrimitiveElement(string name, InterfaceType type) : base(name, type)
        {
        }

        public override void Add(InterfaceElement el)
        {
            Console.WriteLine("Cannot add to a PrimitiveElement");
        }

        public override void Display(int indent)
        {
            Console.WriteLine(new String('-', indent) + " " + _name + " : "+_type);
        }

        public override void Remove(InterfaceElement el)
        {
            Console.WriteLine("Cannot remove from a PrimitiveElement");
        }
    }

    internal class CompositeInterface : InterfaceElement
    {
        private List<InterfaceElement> elements = new List<InterfaceElement>();

        public CompositeInterface(string name, InterfaceType type) : base(name, type)
        {
        }
        public override void Add(InterfaceElement el)
        {
            elements.Add(el);
        }

        public override void Display(int indent)
        {
            Console.WriteLine(new String('-', indent) +
        "+ " + _name + " : " + _type);

            // Display each child element on this node

            foreach (InterfaceElement d in elements)
            {
                d.Display(indent + 2);
            }
        }

        public override void Remove(InterfaceElement el)
        {
            elements.Remove(el);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            InterfaceElement Window = new CompositeInterface("Окно пользовательского интерфейса", InterfaceType.Window);
            Window.Add(new PrimitiveElement("Лого компании", InterfaceType.Picture));
            Window.Add(new PrimitiveElement("Кнопка входа", InterfaceType.Button));
            Window.Add(new PrimitiveElement("Кнопка выхода", InterfaceType.Button));
            Window.Add(new PrimitiveElement("Поле ввода: Логин", InterfaceType.InputField));
            Window.Add(new PrimitiveElement("Поле ввода: Пароль", InterfaceType.InputField));
            var HelpPanel = new CompositeInterface("Справочная панель", InterfaceType.Panel);
            Window.Add(HelpPanel);
            HelpPanel.Add(new PrimitiveElement("Поле текстовой помощи", InterfaceType.InputField));
            HelpPanel.Add(new PrimitiveElement("Кнопка выхода", InterfaceType.Button));
            HelpPanel.Add(new PrimitiveElement("Гиперссылка на сайт разработчика", InterfaceType.HyperLink));
            Window.Display(1);
            Console.ReadLine();
        }
    }
}
