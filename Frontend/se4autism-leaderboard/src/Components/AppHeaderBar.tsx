import React from 'react';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import {Link, NavLink, RouteComponentProps, withRouter} from 'react-router-dom';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      display: 'flex',
      flexGrow: 1,
    },
    menuButton: {
      marginRight: theme.spacing(2),
    },
    title: {
      flexGrow: 1,
      textAlign: 'center'
    },
  }),
);

const AppHeaderBar = (props: RouteComponentProps) => {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <AppBar position="static">
        <Toolbar>
            <Button color={"inherit"} onClick={() => props.history.push('/')}>Home</Button>
          <Typography variant="h6" className={classes.title}>
            CGCC High Score Leaderboard
          </Typography>
        </Toolbar>
      </AppBar>
    </div>
  );
}

export default withRouter(AppHeaderBar)