import { Link } from "react-router-dom";
import { Button, Header, Icon, Segment } from "semantic-ui-react";

export default function NotFound(){
    return(
       <Segment placeholder>
            <Header icon>
                <Icon name='search' />
                Oopps -we've looked everywhere but could not find what you are looking for!
            </Header>
            <Segment.Inline>
                <Button as={Link} to='/activities'>Go back to Activity</Button>
            </Segment.Inline>
       </Segment>
    )
}