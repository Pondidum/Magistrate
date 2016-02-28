import React from 'react'
import UserTile from './usertile'
import CreateUserDialog from './CreateUserDialog'
import Overview from '../overview'

var UserOverview = React.createClass({

  render() {

    return (
      <Overview
        tile={UserTile}
        create={CreateUserDialog}
        collection={this.props.collection}
        {...this.props}
      />
    );

  }
});

export default UserOverview
