var UserOverview = React.createClass({

  render() {

    return (
      <Overview
        url="/api/users"
        tile={UserTile}
        create={CreateUserDialog}
        navigate={this.props.navigate}
      />
    );

  }
});