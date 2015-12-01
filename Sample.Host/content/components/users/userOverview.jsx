var UserOverview = React.createClass({

  render() {

    return (
      <Overview
        url="/api/users/all"
        tile={UserTile}
        create={CreateUserDialog}
      />
    );

  }
});
