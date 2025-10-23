import { makeStyles } from '@material-ui/core';

const useStyles = makeStyles({
  svg: {
    width: 'auto',
    height: 28,
  },
  path: {
    fill: '#7df3e1',
  },
});

const LogoIcon = () => {
  const classes = useStyles();

  return (
    <img
    src="/logoIcon.png"
    alt="Logo"
    className={classes.svg}
  />
  );
};

export default LogoIcon;
