﻿// <copyright file="ZoomInCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-III-04</date>
// <summary>Команда - увеличить.</summary>

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - увеличить.
    /// </summary>
    public sealed class ZoomInCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Если документа не существует, бросаем исключение.
            if (!Core.Instance.IsDocumentsExists)
                throw new InvalidContextException(@"Document not exists.");

            // Изменяем масштаб.
            Core.Instance.CurrentDocument.DocumentSheet.ScaleFactor *= 1.1f;

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}