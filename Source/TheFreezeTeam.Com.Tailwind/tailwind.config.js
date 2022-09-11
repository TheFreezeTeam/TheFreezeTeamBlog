// tailwind.config.js

const defaultTheme = require("tailwindcss/defaultTheme");
const colors = require("tailwindcss/colors");

/** @type {import('tailwindcss').Config} */

module.exports = {
  content: [
    "../TheFreezeTeam.Com/input/**/*.razor",
    "../TheFreezeTeam.Com/input/**/*.razor.cs",
    "../TheFreezeTeam.Com/input/**/*.cshtml",
    "../TheFreezeTeam.Com/input/**/*.md",
    "../TheFreezeTeam.Com/input/**/*.html",
    "../TheFreezeTeam.Com/input/**/*.svg",
  ],
  safelist: [
    {
      pattern:
        /bg-(primary|secondary|accent|danger|warning|positive|gray)-(50|100|200|300|400|500|600|700|800|900)/,
    },
  ],
  theme: {
    extend: {
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
