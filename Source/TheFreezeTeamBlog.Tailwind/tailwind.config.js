const colors = require("tailwindcss/colors");

module.exports = {
  purge: [],
  darkMode: false, // or 'media' or 'class'
  theme: {
    maxHeight: {
      '350Max': '350px',
      '500Max': '500px'
    },
    colors: {
      brandBlue: "#2b59c0",
      transparent: "transparent",
      current: "currentColor",
      black: colors.black,
      white: colors.white,
      gray: colors.coolGray,
      red: colors.red,
      yellow: colors.amber,
      green: colors.emerald,
      blue: colors.blue,
      indigo: colors.indigo,
      purple: colors.violet,
      pink: colors.pink,
    },
    extend: {},
  },
  variants: {
    extend: {
      width: ["responsive", "hover", "focus"],
    },
  },
  plugins: [],
};
