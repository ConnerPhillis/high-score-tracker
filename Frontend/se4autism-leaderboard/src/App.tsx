import React from 'react';
import './App.css';
import { ThemeProvider, createMuiTheme, CssBaseline, makeStyles } from '@material-ui/core';

import AppHeaderBar from './Components/AppHeaderBar'
import GameClient from './Clients/GameClient';
import GameListPage from './Pages/GameListPage';
import { Router, Route, Switch } from 'react-router';
import { BrowserRouter } from 'react-router-dom';
import GamePage from './Pages/GamePage';


const theme = createMuiTheme({
  palette: {
      type: 'dark'
  }
})

const useStyles = makeStyles({
  fillContainer: {
    width: '100%',
    height: '100%',
  }
})

const gameClient = new GameClient()

function App() {

  const styles = useStyles()

  return (
    <div className="app">
    
      <BrowserRouter>
        <ThemeProvider theme={theme}>
          <CssBaseline/>
          <AppHeaderBar></AppHeaderBar>

          <Switch>
            <Route exact path="/">
              <div className={styles.fillContainer}>
                <GameListPage/>
              </div>
            </Route>
            <Route exact path="/:gameId">
              <div className={styles.fillContainer}>
                <GamePage/>
              </div>
            </Route>

          </Switch>


        </ThemeProvider>
      </BrowserRouter>
    </div>
  );
}

export default App;
