var RoleSelector = React.createClass({

  open() {
    this.refs.dialog.open();
  },

  render() {
    return (
      <SelectorDialog
        name="Roles"
        collectionUrl="/api/roles/all"
        ref="dialog"
        {...this.props}
      />
    );
  }

});
