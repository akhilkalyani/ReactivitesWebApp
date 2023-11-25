import { Fragment, useEffect } from 'react';
import { Container} from 'semantic-ui-react';
import Navbar from './Navbar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import LoadingComponent from './LoadingComponent';
import { useStore } from '../stores/store';
import { observer } from 'mobx-react-lite';
// eslint-disable-next-line react-refresh/only-export-components
function App() {
  const {activityStore}=useStore();
  useEffect(()=>{
   activityStore.loadActivities();
  },[activityStore])

  if(activityStore.loadingInitial)return <LoadingComponent content='Loading App' />
  return (
    <Fragment>
      <Navbar />
      <Container style={{marginTop:'7em'}}>
        <ActivityDashboard />
      </Container>
    </Fragment>
  )
}

// eslint-disable-next-line react-refresh/only-export-components
export default observer(App)
