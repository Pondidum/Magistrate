var PermissionSelector = React.createClass({

  open() {
    this.refs.dialog.open();
  },

  render() {
    return (
      <SelectorDialog
        name="Permissions"
        collectionUrl="/api/permissions/all"
        ref="dialog"
        {...this.props}
      />
    );
  }

});
