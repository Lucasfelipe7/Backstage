import { makeStyles } from '@material-ui/core';

const useStyles = makeStyles({
  svg: {
    width: 'auto',
    height: 50,
  },
  path: {
    fill: '#7df3e1',
  },
});
const LogoFull = () => {
  const classes = useStyles();

  return (
    <img
    src="/logo.svg"
    alt="Logo"
    className={classes.svg}
  />
  );
};

export default LogoFull;
