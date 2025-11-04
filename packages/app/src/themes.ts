import {
  createBaseThemeOptions,
  createUnifiedTheme,
  genPageTheme,
  palettes,
  shapes,
} from '@backstage/theme';

export const myTheme = createUnifiedTheme({
  ...createBaseThemeOptions({
    palette: {
      ...palettes.light,
      primary: { main: '#347d6b', light: '#21483fff' },
      secondary: { main: '#155760' },
      success: { main: '#28A745' },
      error: { main: '#DC3545' },
      warning: { main: '#FFC107' },
      text: { primary: '#596763', secondary: '#5e736e' },
      background: { default: '#EAEEEC', paper: '#E3EAE8' },
      banner: {
        info: '#155760',
        error: '#DC3545',
        text: '#596763',
        link: '#dea400',
      },
      navigation: {
        background: '#21483fff',
        indicator: '#155760',
        color: '#E6F4F1',
        selectedColor: '#ffffff',
      },
    },
  }),
  defaultPageTheme: 'home',
  /* header colors */
  pageTheme: {
    home: genPageTheme({ colors: ['#347d6b', '#155760'], shape: shapes.wave }),
    documentation: genPageTheme({ colors: ['#347d6b', '#E6F4F1'], shape: shapes.wave2 }),
    tool: genPageTheme({ colors: ['#28A745', '#155760'], shape: shapes.round }),
    service: genPageTheme({ colors: ['#347D6B', '#155760'], shape: shapes.wave }),
    website: genPageTheme({ colors: ['#FFC107', '#155760'], shape: shapes.wave }),
    library: genPageTheme({ colors: ['#347d6b', '#E6F4F1'], shape: shapes.wave }),
    other: genPageTheme({ colors: ['#347d6b', '#155760'], shape: shapes.wave }),
    app: genPageTheme({ colors: ['#347d6b', '#155760'], shape: shapes.wave }),
    apis: genPageTheme({ colors: ['#28A745', '#155760'], shape: shapes.wave }),
  },
});

export const myDarkTheme = createUnifiedTheme({
  ...createBaseThemeOptions({
    palette: {
      ...palettes.dark,
      primary: { main: '#347d6b', light: '#106669ff' }, 
      secondary: { main: '#347D6B' },
      success: { main: '#28A745' },
      error: { main: '#DC3545' },
      warning: { main: '#FFC107' },
      text: { primary: '#E6F4F1', secondary: '#696969' }, 
      background: { default: '#121212', paper: '#1D1D1D' },
      banner: {
        info: '#347D6B',
        error: '#DC3545',
        text: '#E6F4F1',
        link: '#347D6B',
      },
      navigation: {
        background: '#000000', 
        indicator: '#347D6B',  
        color: '#E6F4F1',      
        selectedColor: '#FFFFFF',
      },
    },
  }),
  defaultPageTheme: 'home',
  pageTheme: {
    home: genPageTheme({ colors: ['#347D6B', '#000000'], shape: shapes.wave }),
    documentation: genPageTheme({ colors: ['#347D6B', '#121212'], shape: shapes.wave2 }),
    tool: genPageTheme({ colors: ['#28A745', '#347D6B'], shape: shapes.round }),
    service: genPageTheme({ colors: ['#347D6B', '#28A745'], shape: shapes.wave }),
    website: genPageTheme({ colors: ['#FFC107', '#347D6B'], shape: shapes.wave }),
    library: genPageTheme({ colors: ['#347D6B', '#121212'], shape: shapes.wave }),
    other: genPageTheme({ colors: ['#347D6B', '#000000'], shape: shapes.wave }),
    app: genPageTheme({ colors: ['#347D6B', '#000000'], shape: shapes.wave }),
    apis: genPageTheme({ colors: ['#28A745', '#347D6B'], shape: shapes.wave }),
  },
});
