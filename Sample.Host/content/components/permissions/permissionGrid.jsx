var PermissionGrid = React.createClass({

  render() {
    return (
      <Grid
        name="permissions"
        tile={PermissionTile}
        selector={PermissionSelector}
        {...this.props}
      />
    );
  }
});
