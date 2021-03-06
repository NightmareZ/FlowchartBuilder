﻿// <copyright file="AlignCenterHorizontalSheetCommand.cs" company="Michael Makarov">
// Copyright Michael Makarov (c) 2009 All Right Reserved
// </copyright>
// <author>Michael Makarov</author>
// <email>m.m.makarov@gmail.com</email>
// <date>2009-V-28</date>
// <summary>Команда - выровнять по центру по горизонтали на листе.</summary>

using System.Collections.Generic;
using Makarov.FlowchartBuilder.Extensions;
using Makarov.FlowchartBuilder.Glyphs;

namespace Makarov.FlowchartBuilder.Commands
{
    /// <summary>
    /// Команда - выровнять по центру по горизонтали на листе.
    /// </summary>
    public sealed class AlignCenterHorizontalSheetCommand : Command
    {
        /// <summary>
        /// Выполнить команду.
        /// </summary>
        public override void Run()
        {
            // Если документа не существует, бросаем исключение.
            if (!Core.Instance.IsDocumentsExists)
                throw new InvalidContextException(@"Document not exists.");

            // Если нет выбранных глифов, бросаем исключение.
            if (!Core.Instance.CurrentDocument.DocumentSheet.SelectedGlyphsExists)
                throw new InvalidContextException(@"Selected glyphs not exists.");

            // Вычисляем дефолтное правое смещение по горизонтали.
            // Берём максимально левое положение на листе.
            int right = 0;

            // Вычисляем дефолтное левое смещение по горизонтали.
            // Берём максимально правое положение на листе.
            var sizedSheet = (ISize)Core.Instance.CurrentDocument.DocumentSheet;
            int left = sizedSheet.Width.MMToPx();

            // Список глифов, которые нужно обработать.
            var lst = new List<BlockGlyph>();

            // Проходим по всем незафиксированным глифам на в текущем листе...
            foreach (var glyph in Core.Instance.CurrentDocument.DocumentSheet.NonFixedGlyphs)
            {
                // Если глиф - блок и он выбран...
                if (glyph is BlockGlyph && glyph.Selected)
                {
                    // Приводим глиф к блоку.
                    var g = (BlockGlyph)glyph;

                    // Если текущий глиф правее текущего смещения,
                    // обновляем текущее смещение.
                    if (g.X + (g.Width >> 1) > right)
                        right = g.X + (g.Width >> 1);

                    // Если текущий глиф левее текущего смещения,
                    // обновляем текущее смещение.
                    if (g.X - (g.Width >> 1) < left)
                        left = g.X - (g.Width >> 1);

                    // Добавляем глиф в список.
                    lst.Add(g);
                }
            }

            // Проходим по всем найденным глифам и смещаем их...
            right = sizedSheet.Width.MMToPx() - right;
            var offset = (right - left) >> 1;
            foreach (var glyph in lst)
                glyph.X += offset;

            // Перерисовываем окно.
            Core.Instance.Redraw();
        }
    }
}