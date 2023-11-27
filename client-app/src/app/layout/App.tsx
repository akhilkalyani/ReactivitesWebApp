import { Fragment } from 'react';
import { Container} from 'semantic-ui-react';
import Navbar from './Navbar';
import { observer } from 'mobx-react-lite';
import { Outlet, useLocation } from 'react-router-dom';
import HomePage from '../../features/home/HomePage';
// eslint-disable-next-line react-refresh/only-export-components
function App() {
  const location=useLocation();
  return (
    <Fragment>
      {location.pathname==='/'?<HomePage />: (
        <>
          <Navbar />
          <Container style={{marginTop:'7em'}}>
            <Outlet />
          </Container>
        </>
      )}
    </Fragment>
  )
}

// eslint-disable-next-line react-refresh/only-export-components
export default observer(App)
