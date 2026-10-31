using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AEC.UI.Utility
{
    public class TextRenderHelper
    {
        public static TextFormatFlags GetTextFormatFromTextAlignmeng(ContentAlignment alignment)
        {
            //TextFormatFlags tff = TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak;
            TextFormatFlags tff = TextFormatFlags.Default;

            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                    tff |= TextFormatFlags.Top | TextFormatFlags.Left;
                    break;

                case ContentAlignment.TopCenter:
                    tff |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;

                case ContentAlignment.TopRight:
                    tff |= TextFormatFlags.Top | TextFormatFlags.Right;
                    break;

                case ContentAlignment.MiddleLeft:
                    tff |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;

                case ContentAlignment.MiddleCenter:
                    tff |= TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter;
                    break;

                case ContentAlignment.MiddleRight:
                    tff |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;

                case ContentAlignment.BottomLeft:
                    tff |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                    break;

                case ContentAlignment.BottomCenter:
                    tff |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;

                case ContentAlignment.BottomRight:
                    tff |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                    break;
            }

            return tff;
        }

        public static void RenderText(Graphics g, string text, Font font, Rectangle rect, Color color, ContentAlignment alignment)
        {
            TextFormatFlags tff = GetTextFormatFromTextAlignmeng(alignment);
            TextRenderer.DrawText(g, text, font, rect, color, tff);
        }
    }
}
