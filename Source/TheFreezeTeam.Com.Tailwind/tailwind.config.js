// tailwind.config.js

const defaultTheme = require("tailwindcss/defaultTheme");
const colors = require("tailwindcss/colors");

/** @type {import('tailwindcss').Config} */

module.exports = {
  content: [
    "../TheFreezeTeam.com/input/**/*.razor",
    "../TheFreezeTeam.com/input/**/*.razor.cs",
    "../TheFreezeTeam.com/input/**/*.cshtml",
    "../TheFreezeTeam.com/input/**/*.md",
    "../TheFreezeTeam.com/input/**/*.html",
    "../TheFreezeTeam.com/input/**/*.svg",
  ],
  safelist: [
    {
      pattern:
        /bg-(primary|secondary|accent|danger|warning|positive|gray)-(50|100|200|300|400|500|600|700|800|900)/,
    },
  ],
  theme: {
    extend: {
      typography: (theme) => ({
        DEFAULT: {
          css: {
            code: {
              '&::before': {
                content: '"" !important'
              },
              '&::after': {
                content: '"" !important'
              }
            }
          }
        }
      }),
      backgroundImage: {
        "the-freeze-team": "url(/images/the-freeze-team.png)",
      },
      colors: {
        primary: {
          DEFAULT: colors.blue,
          dark: colors.blue,
        },
        secondary: {
          DEFAULT: colors.gray,
          dark: colors.gray,
        },
        accent: {
          DEFAULT: colors.teal,
          dark: colors.teal,
        },
        danger: colors.red,
        warning: colors.amber,
        positive: colors.emerald,
      },
      fontFamily: {
        sans: ["Inter var", ...defaultTheme.fontFamily.sans],
      },
    },
  },
  plugins: [
    require("@tailwindcss/typography"),
    require("@tailwindcss/forms"),
    require("@tailwindcss/aspect-ratio"),
    require("@tailwindcss/line-clamp"),
  ],
};
