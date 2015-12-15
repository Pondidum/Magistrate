var UserOverview = React.createClass({

  render() {

    return (
      <Overview
        tile={UserTile}
        create={CreateUserDialog}
        {...this.props}
      />
    );

  }
});
